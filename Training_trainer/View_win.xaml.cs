using Npgsql;
using System;
using System.Windows;

namespace Training_trainer
{
	/// <summary>
	/// Логика взаимодействия для View_win.xaml
	/// </summary>
	public partial class View_win : Window
    {
		Main_win super { get; }

        public View_win(Main_win super, int group_id)
        {
            InitializeComponent();
			this.super = super;
			string sql = "select customer_view_admin.id, sname, fname, pname, age, mail " +
			"from customer_view_admin, \"customer-customer_group\" " +
			"where customer_view_admin.id = \"customer-customer_group\".id_customer and \"customer-customer_group\".id_group = " + group_id;
			NpgsqlCommand comm = new NpgsqlCommand(sql, super.conn);
			NpgsqlDataReader reader;
			try
			{
				super.conn.Open();
				reader = comm.ExecuteReader();
				for (int i = 0; reader.Read(); i++)
				{
					dg_list.Items.Add(new CustomerList(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), (int)reader.GetDouble(4), reader.GetValue(5).ToString()));
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
    }
}
