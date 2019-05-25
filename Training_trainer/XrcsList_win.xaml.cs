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
	/// Логика взаимодействия для XrcsList_win.xaml
	/// </summary>
	public partial class XrcsList_win : Window
	{
		Main_win super { get; }
		Crtgr_win crtgr { get; }
		List<ExerciseList> list { get; set; }

		public XrcsList_win(Crtgr_win crt_win, Main_win super, List<ExerciseList> exerc_list)//Список передаётся для отметок уже добавленных упражнений
		{
			InitializeComponent();
			this.super = super;
			crtgr = crt_win;
			list = exerc_list;
			FillPickTable();
			FillUnpickTable();
		}

		private void FillPickTable()
		{
			foreach (var item in list)
			{
				dg_picked.Items.Add(item);
			}
		}
		private void FillUnpickTable()
		{
			NpgsqlCommand comm = new NpgsqlCommand("select * from exercise", super.conn);
			try
			{
				super.conn.Open();
				NpgsqlDataReader reader = comm.ExecuteReader();
				for (int i = 0; reader.Read(); i++)
				{
					ExerciseList item = new ExerciseList(reader.GetInt32(0), reader.GetString(1));
					if (!list.Exists(it => it.id == reader.GetInt32(0)))
						dg_unpicked.Items.Add(item);
				}
				super.conn.Close();
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
		public void UpdatePickTable()
		{
			dg_picked.Items.Clear();
			FillPickTable();
			crtgr.UpdateList();
		}
		public void UpdateUnpickTable()
		{
			dg_unpicked.Items.Clear();
			FillUnpickTable();
		}

		private void B_crt_exerc_Click(object sender, RoutedEventArgs e)
		{
			CrtXrcs_win win = new CrtXrcs_win(super, this);
			win.Show();
		}

		private void Dg_unpicked_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			ExerciseList selected = dg_unpicked.SelectedItem as ExerciseList;
			list.Add(selected);
			UpdatePickTable();
			UpdateUnpickTable();
		}

		private void Dg_picked_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			ExerciseList selected = dg_picked.SelectedItem as ExerciseList;
			list.Remove(selected);
			UpdatePickTable();
			UpdateUnpickTable();
		}

		private void Dg_unpicked_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{
			b_unpicked_info.IsEnabled = dg_unpicked.SelectedItems.Count != 0;
		}

		private void Dg_picked_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{
			b_picked_info.IsEnabled = dg_picked.SelectedItems.Count != 0;
		}
	}
}
