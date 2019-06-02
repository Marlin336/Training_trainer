using Npgsql;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Training_trainer
{
	/// <summary>
	/// Логика взаимодействия для Main_win.xaml
	/// </summary>
	public partial class Main_win : Window
	{
		private bool logout { set; get; }
		public Login_win super { get; }
		public int user_id { get; }
		public NpgsqlConnection conn { get; }
		public Main_win(Login_win super, int id, string login, string password)
		{
			InitializeComponent();
			this.super = super;
			user_id = id;
			conn = new NpgsqlConnection("Server = 127.0.0.1; Port = 5432; User Id = trainer_" + login + "; Password = " + password + "; Database = Training;");
			FillMyGroupsTable();
			FillAllGroupsTable();
		}

		private void FillMyGroupsTable()
		{
			string sql = "SELECT * FROM public.group_view_admin where trainer_id = " + user_id;
			NpgsqlCommand comm = new NpgsqlCommand(sql, conn);
			NpgsqlDataReader reader;
			try
			{
				conn.Open();
				reader = comm.ExecuteReader();
				for (int i = 0; reader.Read(); i++)
				{
					dg_mygr.Items.Add(new GroupList(reader.GetInt32(0), reader.GetString(2), reader.GetValue(3).ToString(), reader.GetValue(4).ToString(), reader.GetInt32(5), reader.GetInt32(6)));
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
			finally { conn.Close(); }
		}
		private void FillAllGroupsTable()
		{
			string sql = "SELECT * FROM public.group_view_admin";
			NpgsqlCommand comm = new NpgsqlCommand(sql, conn);
			NpgsqlDataReader reader;
			try
			{
				conn.Open();
				reader = comm.ExecuteReader();
				for (int i = 0; reader.Read(); i++)
				{
					dg_allgr.Items.Add(new GroupList(reader.GetInt32(0), reader.GetString(2), reader.GetValue(3).ToString(), reader.GetValue(4).ToString(), reader.GetInt32(5), reader.GetInt32(6)));
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
			finally { conn.Close(); }
		}
		public void UpdateMyGroupsTable()
		{
			dg_mygr.Items.Clear();
			FillMyGroupsTable();
		}
		public void UpdateAllGroupsTable()
		{
			dg_allgr.Items.Clear();
			FillAllGroupsTable();
		}
		#region events
		private void B_logout_Click(object sender, RoutedEventArgs e)
		{
			logout = true;
			Close();
		}
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (logout)
			{
				if (MessageBox.Show("Вы действительно хотите выйти?", "Выйти?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
				{
					super.tb_log.Clear();
					super.tb_pass.Clear();
					super.Show();
				}
				else
				{
					logout = false;
					e.Cancel = true;
				}
			}
			else
			{
				if (MessageBox.Show("Вы действительно хотите закрыть приложение?", "Закрыть приложение?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
					Application.Current.Shutdown();
				else
					e.Cancel = true;
			}
		}
		private void Dg_mygr_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{
			mb_myinfo.IsEnabled = mb_mysub.IsEnabled = mb_del_gr.IsEnabled = dg_mygr.SelectedCells.Count != 0;
		}
		private void Dg_allgr_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{
			mb_allinfo.IsEnabled = mb_allsub.IsEnabled = dg_allgr.SelectedCells.Count != 0;
		}
		private void Mb_crt_gr_Click(object sender, RoutedEventArgs e)
		{
			Crtgr_win win = new Crtgr_win(this);
			win.Show();
		}
		private void Mb_del_gr_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show("Вы уверены что хотите распустить эту группу?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
			{
				GroupList group = dg_mygr.SelectedItem as GroupList;
				string sql = "DELETE FROM customer_group WHERE id = " + group.id + "; ";
				NpgsqlCommand comm = new NpgsqlCommand(sql, conn);
				try
				{
					conn.Open();
					comm.ExecuteNonQuery();
					conn.Close();
					UpdateMyGroupsTable();
				}
				catch (NpgsqlException ex)
				{
					MessageBox.Show(ex.Message, "Ошибка на сервере", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
				}
				finally { conn.Close(); }
			}
		}
		private void Mb_all_update_Click(object sender, RoutedEventArgs e)
		{
			UpdateAllGroupsTable();
		}
		private void Mb_my_update_Click(object sender, RoutedEventArgs e)
		{
			UpdateMyGroupsTable();
		}
		private void Mb_myinfo_Click(object sender, RoutedEventArgs e)
		{
			Crtgr_win win = new Crtgr_win(this, dg_mygr.SelectedItem as GroupList, true);
			win.Show();
		}
		private void Mb_allinfo_Click(object sender, RoutedEventArgs e)
		{
			Crtgr_win win = new Crtgr_win(this, dg_allgr.SelectedItem as GroupList, false);
			win.Show();
		}
		private void Mb_mysub_Click(object sender, RoutedEventArgs e)
		{
			GroupList group = dg_mygr.SelectedItem as GroupList;
			View_win win = new View_win(this, group.id);
			win.Show();
		}
		private void Mb_allsub_Click(object sender, RoutedEventArgs e)
		{
			GroupList group = dg_allgr.SelectedItem as GroupList;
			View_win win = new View_win(this, group.id);
			win.Show();
		}
		private void B_profile_Click(object sender, RoutedEventArgs e)
		{
			Profile_win win = new Profile_win(this);
			win.Show();
		}
		#endregion
	}
}
