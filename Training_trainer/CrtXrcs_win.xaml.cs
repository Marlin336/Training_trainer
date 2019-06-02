using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Training_trainer
{
	/// <summary>
	/// Логика взаимодействия для CrtXrcs_win.xaml
	/// </summary>
	public partial class CrtXrcs_win : Window
	{
		private class XTreeViewItem : TreeViewItem
		{
			public int id { get; }
			public XTreeViewItem(int id, string header)
			{
				this.id = id;
				Header = header;
			}
		}
		private class XCheckBox : CheckBox
		{
			public int id { get; }
			public XCheckBox(int id, string content)
			{
				this.id = id;
				Content = content;
			}
		}
		Main_win super { get; }
		XrcsList_win table_win { get; }

		public CrtXrcs_win(Main_win super, XrcsList_win table)
		{
			InitializeComponent();
			this.super = super;
			table_win = table;
			NpgsqlCommand comm = new NpgsqlCommand("select group_id, group_name from muscle_view group by group_id, group_name order by group_id", super.conn);
			try
			{
				super.conn.Open();
				NpgsqlDataReader reader = comm.ExecuteReader();
				List<XTreeViewItem> list = new List<XTreeViewItem>();
				for (int i = 0; reader.Read(); i++)
				{
					XTreeViewItem item = new XTreeViewItem(reader.GetInt32(0), reader.GetString(1));
					list.Add(item);
				}
				for (int i = 0; i < list.Count; i++)
				{
					string sql = "select muscle_id, muscle_name from muscle_view where group_id = " + list[i].id + " group by muscle_id, muscle_name ";
					comm = new NpgsqlCommand(sql, super.conn);
					super.conn.Close();
					super.conn.Open();
					reader = comm.ExecuteReader();
					for (int j = 0; reader.Read() ; j++)
						list[i].Items.Add(new XCheckBox(reader.GetInt32(0), reader.GetString(1)));
					tv_main.Items.Add(list[i]);
				}
			}
			catch (NpgsqlException ex)
			{
				MessageBox.Show(ex.Message, "Ошибка на сервере", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
			}
			finally { super.conn.Close(); }
		}
		public CrtXrcs_win(Main_win super, ExerciseList exercise)
		{
			InitializeComponent();
			b_accept.Visibility = Visibility.Collapsed;
			gb_muscle.Margin = new Thickness(10, 70, 10, 10);
			tb_name.IsReadOnly = true;
			tb_name.Text = exercise.name;
			Title = "Информация об упражнении";
			this.super = super;
			List<XTreeViewItem> groups = new List<XTreeViewItem>();
			string sql = "select group_id, group_name from muscle_view, \"exercise-muscle\" where \"exercise-muscle\".id_muscle = muscle_view.muscle_id and \"exercise-muscle\".id_exercise = " + exercise.id + " group by group_id, group_name";
			NpgsqlCommand comm = new NpgsqlCommand(sql, super.conn);
			try
			{
				super.conn.Open();
				NpgsqlDataReader reader = comm.ExecuteReader();
				for (int i = 0; reader.Read(); i++)
					groups.Add(new XTreeViewItem(reader.GetInt32(0), reader.GetString(1)));
				super.conn.Close();
				comm.CommandText = "Select group_id, muscle_id, muscle_name from muscle_view, \"exercise-muscle\" where \"exercise-muscle\".id_muscle = muscle_view.muscle_id and \"exercise-muscle\".id_exercise = " + exercise.id;
				super.conn.Open();
				reader = comm.ExecuteReader();
				for (int i = 0; reader.Read(); i++) 
					groups.Find(gr => gr.id == reader.GetInt32(0)).Items.Add(new XTreeViewItem(reader.GetInt32(1), reader.GetString(2)));
				super.conn.Close();
				foreach (var item in groups)
					tv_main.Items.Add(item);
			}
			catch (NpgsqlException ex)
			{
				MessageBox.Show(ex.Message, "Ошибка на сервере", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
			}
			finally { super.conn.Close(); }

		}

		private void B_accept_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				NpgsqlCommand comm = new NpgsqlCommand("select exercise_add('" + tb_name.Text + "')", super.conn);
				super.conn.Open();
				int exercise_id = (int)comm.ExecuteScalar();
				super.conn.Close();
				List<int> muscleList = new List<int>();
				for (int i = 0; i < tv_main.Items.Count; i++)
				{
					var group = tv_main.Items[i] as XTreeViewItem;
					for (int j = 0; j < group.Items.Count; j++)
					{
						var muscle = group.Items[j] as XCheckBox;
						if (muscle.IsChecked.Value)
							muscleList.Add(muscle.id);
					}
				}
				for (int i = 0; i < muscleList.Count; i++)
				{
					comm = new NpgsqlCommand("INSERT INTO public.\"exercise-muscle\"(id_exercise, id_muscle) VALUES(" + exercise_id + ", " + muscleList[i] + ");", super.conn);
					super.conn.Open();
					comm.ExecuteNonQuery();
					super.conn.Close();
				}
				table_win.UpdateUnpickTable();
				Close();
			}
			catch (NpgsqlException ex)
			{
				MessageBox.Show(ex.Message, "Ошибка на сервере", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
			}
			finally { super.conn.Close(); }
		}
	}
}
