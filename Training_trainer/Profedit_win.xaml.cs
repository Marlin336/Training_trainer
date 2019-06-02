using Npgsql;
using System;
using System.Windows;

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
			string sql = "select login, pass, mail, fname, sname, pname from my_own_trainer(" + super.user_id + ")";
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
				tb_fname.Text = reader.GetString(3);
				tb_sname.Text = reader.GetString(4);
				tb_pname.Text = reader.GetString(5);
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
		#region events
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
					string sql = "UPDATE trainer SET first_name = '" + tb_fname.Text + "', second_name = '" + tb_sname.Text + "', parent_name = '" + tb_pname.Text + "', e_mail = '" + tb_mail.Text + "', login = '" + tb_login.Text + "', pass = '" + tb_pass.Password + "' WHERE id = " + super.user_id;
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
					prof_win.l_name.Content = tb_sname.Text + " " + tb_fname.Text + " " + tb_pname.Text;
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
		#endregion
	}
}
