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
    /// Логика взаимодействия для Crtgr_win.xaml
    /// </summary>
    public partial class Crtgr_win : Window
    {
		public Main_win super { get; }
		private bool[] week = new bool[7];
		private string[] timetable = new string[7];
		public List<ExerciseList> exerciseList = new List<ExerciseList>();
        public Crtgr_win(Main_win super)
        {
            InitializeComponent();
			this.super = super;
        }

		public void UpdateList()
		{
			dg_list.Items.Clear();
			foreach (var item in exerciseList)
				dg_list.Items.Add(item);
		}

		#region checkbox_checking
		private void Cb_mon_Checked(object sender, RoutedEventArgs e)
		{
			tp_mon.IsEnabled = true;
			tp_mon.Value = DateTime.Parse("12:00");
		}
		private void Cb_tue_Checked(object sender, RoutedEventArgs e)
		{
			tp_tue.IsEnabled = true;
			tp_tue.Value = DateTime.Parse("12:00");
		}
		private void Cb_wed_Checked(object sender, RoutedEventArgs e)
		{
			tp_wed.IsEnabled = true;
			tp_wed.Value = DateTime.Parse("12:00");
		}
		private void Cb_thu_Checked(object sender, RoutedEventArgs e)
		{
			tp_thu.IsEnabled = true;
			tp_thu.Value = DateTime.Parse("12:00");
		}
		private void Cb_fri_Checked(object sender, RoutedEventArgs e)
		{
			tp_fri.IsEnabled = true;
			tp_fri.Value = DateTime.Parse("12:00");
		}
		private void Cb_sat_Checked(object sender, RoutedEventArgs e)
		{
			tp_sat.IsEnabled = true;
			tp_sat.Value = DateTime.Parse("12:00");
		}
		private void Cb_sun_Checked(object sender, RoutedEventArgs e)
		{
			tp_sun.IsEnabled = true;
			tp_sun.Value = DateTime.Parse("12:00");
		}
		private void Cb_mon_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_mon.IsEnabled = false;
			tp_mon.Value = null;
		}
		private void Cb_tue_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_tue.IsEnabled = false;
			tp_tue.Value = null;
		}
		private void Cb_wed_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_wed.IsEnabled = false;
			tp_wed.Value = null;
		}
		private void Cb_thu_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_thu.IsEnabled = false;
			tp_thu.Value = null;
		}
		private void Cb_fri_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_fri.IsEnabled = false;
			tp_fri.Value = null;
		}
		private void Cb_sat_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_sat.IsEnabled = false;
			tp_sat.Value = null;
		}
		private void Cb_sun_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_sun.IsEnabled = false;
			tp_sun.Value = null;
		}
		private void Cb_minage_Checked(object sender, RoutedEventArgs e)
		{
			num_minage.IsEnabled = true;
		}
		private void Cb_maxage_Checked(object sender, RoutedEventArgs e)
		{
			num_maxage.IsEnabled = true;
		}
		private void Cb_minage_Unchecked(object sender, RoutedEventArgs e)
		{
			num_minage.IsEnabled = false;
		}
		private void Cb_maxage_Unchecked(object sender, RoutedEventArgs e)
		{
			num_maxage.IsEnabled = false;
		}
		#endregion

		private void B_add_exerc_Click(object sender, RoutedEventArgs e)
		{
			XrcsList_win win = new XrcsList_win(this, super, exerciseList);
			win.Show();
		}

		private void S_save_Click(object sender, RoutedEventArgs e)
		{
			#region cast
			week[0] = cb_mon.IsChecked.Value;
			week[1] = cb_tue.IsChecked.Value;
			week[2] = cb_wed.IsChecked.Value;
			week[3] = cb_thu.IsChecked.Value;
			week[4] = cb_fri.IsChecked.Value;
			week[5] = cb_sat.IsChecked.Value;
			week[6] = cb_sun.IsChecked.Value;

			timetable[0] = tp_mon.Text.Length != 0 ? "\"" + tp_mon.Text + "\"" : "null";
			timetable[1] = tp_tue.Text.Length != 0 ? "\"" + tp_tue.Text + "\"" : "null";
			timetable[2] = tp_wed.Text.Length != 0 ? "\"" + tp_wed.Text + "\"" : "null";
			timetable[3] = tp_thu.Text.Length != 0 ? "\"" + tp_thu.Text + "\"" : "null";
			timetable[4] = tp_fri.Text.Length != 0 ? "\"" + tp_fri.Text + "\"" : "null";
			timetable[5] = tp_sat.Text.Length != 0 ? "\"" + tp_sat.Text + "\"" : "null";
			timetable[6] = tp_sun.Text.Length != 0 ? "\"" + tp_sun.Text + "\"" : "null";
			#endregion
			string timetable_str = "'{" + timetable[0] + ", " + timetable[1] + ", " + timetable[2] + ", " + timetable[3] + ", " + timetable[4] + ", " + timetable[5] + ", " + timetable[6] + "}'";
			string week_str = "'{" + week[0] + ", " + week[1] + ", " + week[2] + ", " + week[3] + ", " + week[4] + ", " + week[5] + ", " + week[6] + "}'";
			string min_age = cb_minage.IsChecked.Value ? num_minage.Value.ToString() : "null";
			string max_age = cb_maxage.IsChecked.Value ? num_maxage.Value.ToString() : "null";

			string sql = "select group_add(" + super.user_id + ", " + (int)num_cost.Value + ", " + min_age + ", " + max_age + ", " + timetable_str + ", " + week_str + ")";
			NpgsqlCommand comm = new NpgsqlCommand(sql, super.conn);
			try
			{
				super.conn.Open();
				int group_id = (int)comm.ExecuteScalar();
				super.conn.Close();
				for (int i = 0; i < exerciseList.Count; i++)
				{
					sql = "INSERT INTO \"customer_group-exercise\"(id_group, id_exercise) VALUES(" + group_id + ", " + exerciseList[i].id + ");";
					comm.CommandText = sql;
					super.conn.Open();
					comm.ExecuteNonQuery();
					super.conn.Close();
				}
				Close();
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
