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
			conn = new NpgsqlConnection("Server = 127.0.0.1; Port = 5432; User Id = " + login + "; Password = " + password + "; Database = Training;");
			FillMyGroupsTable();
			FillAllGroupsTable();
		}

		private void FillMyGroupsTable()
		{

		}
		private void FillAllGroupsTable()
		{

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
	}
}
