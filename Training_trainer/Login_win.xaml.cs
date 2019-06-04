using Npgsql;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Training_trainer
{
	/// <summary>
	/// Логика взаимодействия для Login_win.xaml
	/// </summary>
	public partial class Login_win : Window
	{
		public Login_win()
		{
			InitializeComponent();
		}
		#region events
		private void Tb_log_TextChanged(object sender, TextChangedEventArgs e)
		{
			b_ent.IsEnabled = tb_log.Text != "" && tb_pass.Password != "";
		}
		private void Tb_pass_PasswordChanged(object sender, RoutedEventArgs e)
		{
			b_ent.IsEnabled = tb_log.Text != "" && tb_pass.Password != "";
		}
		private void B_ent_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				string conn_param = "Server=127.0.0.1;Port=5432;User Id=Training_login;Password=0000;Database=Training;";
				string sql = "select login_trainer('" + tb_log.Text + "', '" + tb_pass.Password + "')";
				NpgsqlConnection conn = new NpgsqlConnection(conn_param);
				NpgsqlCommand comm = new NpgsqlCommand(sql, conn);
				conn.Open();
				int result = (int)comm.ExecuteScalar();
				conn.Close();
				if (result == -1)
				{
					MessageBox.Show("Пользователя с таким логином не существует", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Asterisk);
				}
				else
				{
					if (result == -2)
					{
						MessageBox.Show("Пара логин-пароль не совпадают", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Warning);
					}
					else
					{
						Main_win main = new Main_win(this, result, tb_log.Text, tb_pass.Password);
						main.Show();
						Hide();
					}
				}
			}catch (Exception ex) { MessageBox.Show(ex.Message); }
		}
		#endregion
	}
}
