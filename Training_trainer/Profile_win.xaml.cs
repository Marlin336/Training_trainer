using Npgsql;
using System;
using System.Windows;

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
