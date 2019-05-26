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
	/// Логика взаимодействия для Profile_win.xaml
	/// </summary>
	public partial class Profile_win : Window
	{
		public Main_win super { get; }
		public Profile_win(Main_win super)
		{
			InitializeComponent();
			this.super = super;
			NpgsqlCommand comm = new NpgsqlCommand("select * from my_own_trainer(" + super.user_id + ")", super.conn);
			try
			{
				super.conn.Open();
				NpgsqlDataReader reader = comm.ExecuteReader();
				reader.Read();
				l_name.Content = reader.GetString(2) + " " + reader.GetString(1) + " " + reader.GetString(3);
				l_birthday.Content += reader.GetDate(4).ToString();
				l_mail.Content += reader.GetValue(5).ToString();
				l_login.Content += reader.GetString(6);
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

		private void B_edit_Click(object sender, RoutedEventArgs e)
		{
			Passreq win = new Passreq(super, this);
			win.Show();
		}
	}
}
