using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Npgsql;

namespace Training_trainer
{
    /// <summary>
    /// Логика взаимодействия для Crtgr_win.xaml
    /// </summary>
    public partial class Crtgr_win : Window
    {
		public Main_win super { get; }
		private bool[] week = new bool[7];
		private string[] timetable = new string[7];
		public List<ExerciseList> exerciseList = new List<ExerciseList>();
		private int group_id;
        public Crtgr_win(Main_win super)
        {
            InitializeComponent();
			this.super = super;
			b_save.Click += B_save_Click_1;
        }

		public Crtgr_win(Main_win super, GroupList group, bool edit)
		{
			InitializeComponent();
			this.super = super;
			Title = "Тренер: " + group.trainer;
			b_save.Click += B_save_Click_2;
			NpgsqlCommand comm = new NpgsqlCommand("select * from customer_group where id = " + group.id, super.conn);
			group_id = group.id;
			try
			{
				super.conn.Open();
				NpgsqlDataReader reader = comm.ExecuteReader();
				reader.Read();
				var timestamps = (TimeSpan[])reader.GetValue(5);
				week = (bool[])reader.GetValue(6);
				for (int i = 0; i < timestamps.Length; i++)
				{
					if (week[i])
					{
						timetable[i] = timestamps[i].Hours.ToString() + ":" + (timestamps[i].Minutes.ToString().Length == 1 ? "0" + timestamps[i].Minutes.ToString() : timestamps[i].Minutes.ToString());
					}
				}
				int min_age_db;
				int max_age_db;
				try { min_age_db = reader.GetInt32(3); } catch { min_age_db = 0; }
				try { max_age_db = reader.GetInt32(4); } catch { max_age_db = 0; }
				num_cost.Text = reader.GetInt32(2).ToString();
				num_minage.Text = min_age_db.ToString();
				num_maxage.Text = max_age_db.ToString();
				cb_minage.IsChecked = !num_minage.Text.Equals("0");
				cb_maxage.IsChecked = !num_maxage.Text.Equals("0");

				super.conn.Close();
				comm.CommandText = "select exercise.id, exercise.name " +
				"from exercise, \"customer_group-exercise\" " +
				"where \"customer_group-exercise\".id_exercise = exercise.id and \"customer_group-exercise\".id_group = " + group.id;
				super.conn.Open();
				reader = comm.ExecuteReader();
				for (int i = 0; reader.Read(); i++)
				{
					exerciseList.Add(new ExerciseList(reader.GetInt32(0), reader.GetString(1)));
				}
				super.conn.Close();
				UpdateList();
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
			
			cb_mon.IsChecked = week[0];
			cb_tue.IsChecked = week[1];
			cb_wed.IsChecked = week[2];
			cb_thu.IsChecked = week[3];
			cb_fri.IsChecked = week[4];
			cb_sat.IsChecked = week[5];
			cb_sun.IsChecked = week[6];

			tp_mon.Text = timetable[0];
			tp_tue.Text = timetable[1];
			tp_wed.Text = timetable[2];
			tp_thu.Text = timetable[3];
			tp_fri.Text = timetable[4];
			tp_sat.Text = timetable[5];
			tp_sun.Text = timetable[6];

			if (!edit)
			{
				cb_mon.IsEnabled = cb_tue.IsEnabled = cb_wed.IsEnabled = cb_thu.IsEnabled = cb_fri.IsEnabled = cb_sat.IsEnabled = cb_sun.IsEnabled = false;
				tp_mon.AllowSpin = tp_mon.AllowTextInput = false;
				tp_tue.AllowSpin = tp_tue.AllowTextInput = false;
				tp_wed.AllowSpin = tp_wed.AllowTextInput = false;
				tp_thu.AllowSpin = tp_thu.AllowTextInput = false;
				tp_fri.AllowSpin = tp_fri.AllowTextInput = false;
				tp_sat.AllowSpin = tp_sat.AllowTextInput = false;
				tp_sun.AllowSpin = tp_sun.AllowTextInput = false;
				num_cost.AllowSpin = num_cost.AllowTextInput = false;
				num_maxage.AllowSpin = num_maxage.AllowTextInput = false;
				num_minage.AllowSpin = num_minage.AllowTextInput = false;
				cb_minage.IsEnabled = cb_maxage.IsEnabled = false;
				b_add_exerc.Visibility = Visibility.Collapsed;
				dg_list.Margin = new Thickness(0);
				b_save.Visibility = Visibility.Collapsed;
				l_cost.Margin = new Thickness(10, 0, 0, 35);
				num_cost.Margin = new Thickness(116, 0, 0, 35);
				gb_age.Margin = new Thickness(10, 0, 0, 75);
				gb_timetable.Margin = new Thickness(10, 0, 0, 160);
			}
			else
			{

			}
		}

		public void UpdateList()
		{
			dg_list.Items.Clear();
			foreach (var item in exerciseList)
				dg_list.Items.Add(item);
		}

		#region checkbox_checking
		private void Cb_mon_Checked(object sender, RoutedEventArgs e)
		{
			tp_mon.IsEnabled = true;
			tp_mon.Value = DateTime.Parse("12:00");
		}
		private void Cb_tue_Checked(object sender, RoutedEventArgs e)
		{
			tp_tue.IsEnabled = true;
			tp_tue.Value = DateTime.Parse("12:00");
		}
		private void Cb_wed_Checked(object sender, RoutedEventArgs e)
		{
			tp_wed.IsEnabled = true;
			tp_wed.Value = DateTime.Parse("12:00");
		}
		private void Cb_thu_Checked(object sender, RoutedEventArgs e)
		{
			tp_thu.IsEnabled = true;
			tp_thu.Value = DateTime.Parse("12:00");
		}
		private void Cb_fri_Checked(object sender, RoutedEventArgs e)
		{
			tp_fri.IsEnabled = true;
			tp_fri.Value = DateTime.Parse("12:00");
		}
		private void Cb_sat_Checked(object sender, RoutedEventArgs e)
		{
			tp_sat.IsEnabled = true;
			tp_sat.Value = DateTime.Parse("12:00");
		}
		private void Cb_sun_Checked(object sender, RoutedEventArgs e)
		{
			tp_sun.IsEnabled = true;
			tp_sun.Value = DateTime.Parse("12:00");
		}
		private void Cb_mon_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_mon.IsEnabled = false;
			tp_mon.Value = null;
		}
		private void Cb_tue_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_tue.IsEnabled = false;
			tp_tue.Value = null;
		}
		private void Cb_wed_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_wed.IsEnabled = false;
			tp_wed.Value = null;
		}
		private void Cb_thu_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_thu.IsEnabled = false;
			tp_thu.Value = null;
		}
		private void Cb_fri_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_fri.IsEnabled = false;
			tp_fri.Value = null;
		}
		private void Cb_sat_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_sat.IsEnabled = false;
			tp_sat.Value = null;
		}
		private void Cb_sun_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_sun.IsEnabled = false;
			tp_sun.Value = null;
		}
		private void Cb_minage_Checked(object sender, RoutedEventArgs e)
		{
			num_minage.IsEnabled = true;
		}
		private void Cb_maxage_Checked(object sender, RoutedEventArgs e)
		{
			num_maxage.IsEnabled = true;
		}
		private void Cb_minage_Unchecked(object sender, RoutedEventArgs e)
		{
			num_minage.IsEnabled = false;
		}
		private void Cb_maxage_Unchecked(object sender, RoutedEventArgs e)
		{
			num_maxage.IsEnabled = false;
		}
		#endregion

		private void B_add_exerc_Click(object sender, RoutedEventArgs e)
		{
			XrcsList_win win = new XrcsList_win(this, super, exerciseList);
			win.Show();
		}

		private void B_save_Click_1(object sender, RoutedEventArgs e)
		{
			#region cast
			week[0] = cb_mon.IsChecked.Value;
			week[1] = cb_tue.IsChecked.Value;
			week[2] = cb_wed.IsChecked.Value;
			week[3] = cb_thu.IsChecked.Value;
			week[4] = cb_fri.IsChecked.Value;
			week[5] = cb_sat.IsChecked.Value;
			week[6] = cb_sun.IsChecked.Value;

			timetable[0] = tp_mon.Text.Length != 0 ? "\"" + tp_mon.Text + "\"" : "null";
			timetable[1] = tp_tue.Text.Length != 0 ? "\"" + tp_tue.Text + "\"" : "null";
			timetable[2] = tp_wed.Text.Length != 0 ? "\"" + tp_wed.Text + "\"" : "null";
			timetable[3] = tp_thu.Text.Length != 0 ? "\"" + tp_thu.Text + "\"" : "null";
			timetable[4] = tp_fri.Text.Length != 0 ? "\"" + tp_fri.Text + "\"" : "null";
			timetable[5] = tp_sat.Text.Length != 0 ? "\"" + tp_sat.Text + "\"" : "null";
			timetable[6] = tp_sun.Text.Length != 0 ? "\"" + tp_sun.Text + "\"" : "null";
			#endregion
			string timetable_str = "'{" + timetable[0] + ", " + timetable[1] + ", " + timetable[2] + ", " + timetable[3] + ", " + timetable[4] + ", " + timetable[5] + ", " + timetable[6] + "}'";
			string week_str = "'{" + week[0] + ", " + week[1] + ", " + week[2] + ", " + week[3] + ", " + week[4] + ", " + week[5] + ", " + week[6] + "}'";
			string min_age = cb_minage.IsChecked.Value ? num_minage.Value.ToString() : "null";
			string max_age = cb_maxage.IsChecked.Value ? num_maxage.Value.ToString() : "null";
			string sql = "select group_add(" + super.user_id + ", " + (int)num_cost.Value + ", " + min_age + ", " + max_age + ", " + timetable_str + ", " + week_str + ")";
			NpgsqlCommand comm = new NpgsqlCommand(sql, super.conn);
			try
			{
				super.conn.Open();
				int group_id = (int)comm.ExecuteScalar();
				super.conn.Close();
				for (int i = 0; i < exerciseList.Count; i++)
				{
					sql = "INSERT INTO \"customer_group-exercise\"(id_group, id_exercise) VALUES(" + group_id + ", " + exerciseList[i].id + ");";
					comm.CommandText = sql;
					super.conn.Open();
					comm.ExecuteNonQuery();
					super.conn.Close();
				}
				super.UpdateMyGroupsTable();
				super.UpdateAllGroupsTable();
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
		private void B_save_Click_2(object sender, RoutedEventArgs e)
		{
			#region cast
			week[0] = cb_mon.IsChecked.Value;
			week[1] = cb_tue.IsChecked.Value;
			week[2] = cb_wed.IsChecked.Value;
			week[3] = cb_thu.IsChecked.Value;
			week[4] = cb_fri.IsChecked.Value;
			week[5] = cb_sat.IsChecked.Value;
			week[6] = cb_sun.IsChecked.Value;

			timetable[0] = tp_mon.Text.Length != 0 ? "\"" + tp_mon.Text + "\"" : "null";
			timetable[1] = tp_tue.Text.Length != 0 ? "\"" + tp_tue.Text + "\"" : "null";
			timetable[2] = tp_wed.Text.Length != 0 ? "\"" + tp_wed.Text + "\"" : "null";
			timetable[3] = tp_thu.Text.Length != 0 ? "\"" + tp_thu.Text + "\"" : "null";
			timetable[4] = tp_fri.Text.Length != 0 ? "\"" + tp_fri.Text + "\"" : "null";
			timetable[5] = tp_sat.Text.Length != 0 ? "\"" + tp_sat.Text + "\"" : "null";
			timetable[6] = tp_sun.Text.Length != 0 ? "\"" + tp_sun.Text + "\"" : "null";
			#endregion
			string timetable_str = "'{" + timetable[0] + ", " + timetable[1] + ", " + timetable[2] + ", " + timetable[3] + ", " + timetable[4] + ", " + timetable[5] + ", " + timetable[6] + "}'";
			string week_str = "'{" + week[0] + ", " + week[1] + ", " + week[2] + ", " + week[3] + ", " + week[4] + ", " + week[5] + ", " + week[6] + "}'";
			string min_age = cb_minage.IsChecked.Value ? num_minage.Value.ToString() : "null";
			string max_age = cb_maxage.IsChecked.Value ? num_maxage.Value.ToString() : "null";
			string sql = "UPDATE customer_group	SET cost ="+(int)num_cost.Value+", min_age ="+min_age+", max_age ="+max_age+", timetable ="+timetable_str+", week ="+week_str+" WHERE id = "+group_id+";" ;
			NpgsqlCommand comm = new NpgsqlCommand(sql, super.conn);
			try
			{
				super.conn.Open();
				comm.ExecuteNonQuery();
				super.conn.Close();
				sql = "DELETE FROM \"customer_group-exercise\" WHERE id_group = " + group_id;
				comm.CommandText = sql;
				super.conn.Open();
				comm.ExecuteNonQuery();
				super.conn.Close();
				for (int i = 0; i < exerciseList.Count; i++)
				{
					sql = "INSERT INTO \"customer_group-exercise\"(id_group, id_exercise) VALUES(" + group_id + ", " + exerciseList[i].id + ");";
					comm.CommandText = sql;
					super.conn.Open();
					comm.ExecuteNonQuery();
					super.conn.Close();
				}
				super.UpdateMyGroupsTable();
				super.UpdateAllGroupsTable();
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

		private void Dg_list_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			CrtXrcs_win win = new CrtXrcs_win(super, dg_list.SelectedItem as ExerciseList);
			win.Show();
		}
	}
}
