using Npgsql;
using System;
using System.Windows;

namespace Training_trainer
{
	/// <summary>
	/// Логика взаимодействия для Passreq.xaml
	/// </summary>
	public partial class Passreq : Window
    {
		public Main_win super { get; }
		public Profile_win prof { get; }

        public Passreq(Main_win super, Profile_win prof)
        {
			this.super = super;
			this.prof = prof;
            InitializeComponent();
        }

		private void B_accept_Click(object sender, RoutedEventArgs e)
		{
			string sql = "select pass from my_own_trainer(" + super.user_id + ")";
			NpgsqlCommand comm = new NpgsqlCommand(sql, super.conn);
			try
			{
				super.conn.Open();
				string compare = (string)comm.ExecuteScalar();
				super.conn.Close();
				if (tb_pass.Password == compare)
				{
					Profedit_win win = new Profedit_win(super, prof);
					Close();
					win.ShowDialog();
				}
				else
					MessageBox.Show("Неверный пароль", "Ошибка подтверждения", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK);
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
