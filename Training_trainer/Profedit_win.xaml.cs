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
	/// Логика взаимодействия для Profedit_win.xaml
	/// </summary>
	public partial class Profedit_win : Window
	{
		Main_win super { get; }
		Profile_win prof_win { get; }
		string old_login { get; }
		public Profedit_win(Main_win super, Profile_win profile)
		{
			this.super = super;
			prof_win = profile;
			InitializeComponent();
			string sql = "select login, pass, mail from my_own_trainer(" + super.user_id + ")";
			NpgsqlCommand comm = new NpgsqlCommand(sql, super.conn);
			try
			{
				super.conn.Open();
				NpgsqlDataReader reader = comm.ExecuteReader();
				reader.Read();
				old_login = tb_login.Text = reader.GetString(0);
				tb_pass.Password = reader.GetString(1);
				tb_repass.Password = reader.GetString(1);
				tb_mail.Text = reader.GetValue(2).ToString();
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

		private void Tb_pass_GotFocus(object sender, RoutedEventArgs e)
		{
			tb_repass.Clear();
		}

		private void B_ok_Click(object sender, RoutedEventArgs e)
		{
			if (tb_pass.Password == tb_repass.Password)
			{
				try
				{
					if (tb_login.Text.Trim() == "" || tb_pass.Password.Trim() == "") 
					{
						throw new ArgumentNullException();
					}
					string sql = "UPDATE trainer SET e_mail = '" + tb_mail.Text + "', login = '" + tb_login.Text + "', pass = '" + tb_pass.Password + "' WHERE id = " + super.user_id;
					NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;User Id=Training_login;Password=0000;Database=Training;");
					NpgsqlCommand comm = new NpgsqlCommand(sql, conn);
					conn.Open();
					comm.ExecuteNonQuery();
					if (old_login != tb_login.Text)
					{
						comm.CommandText = "ALTER USER \"trainer_" + old_login + "\" RENAME TO \"trainer_" + tb_login.Text + "\";";
						comm.ExecuteNonQuery();
					}
					comm.CommandText = "ALTER USER \"trainer_" + tb_login.Text + "\" PASSWORD '" + tb_pass.Password + "'; ";
					comm.ExecuteNonQuery();
					conn.Close();
					prof_win.l_login.Content = "Логин: " + tb_login.Text;
					prof_win.l_mail.Content = "E-mail: " + tb_mail.Text;
					Close();
				}
				catch (NpgsqlException ex)
				{
					MessageBox.Show(ex.Message, "Ошибка на сервере", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
				}
				catch (ArgumentNullException)
				{
					MessageBox.Show("Необходимые поля не заполнены",null, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
				}
				finally { super.conn.Close(); }
			}
			else
				MessageBox.Show("Пароли не совпадают", null, MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK);
		}
	}
}
