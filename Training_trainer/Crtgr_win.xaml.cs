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

namespace Training_trainer
{
    /// <summary>
    /// Логика взаимодействия для Crtgr_win.xaml
    /// </summary>
    public partial class Crtgr_win : Window
    {
		public Main_win super { get; }
		private bool[] week = new bool[7];
		public List<int> exerciseList = new List<int>();
        public Crtgr_win(Main_win super)
        {
            InitializeComponent();
			this.super = super;
        }


		#region checkbox_checking
		private void Cb_mon_Checked(object sender, RoutedEventArgs e)
		{
			tp_mon.IsEnabled = true;
			tp_mon.Value = DateTime.Parse("12:00");
			week[0] = true;
		}
		private void Cb_tue_Checked(object sender, RoutedEventArgs e)
		{
			tp_tue.IsEnabled = true;
			tp_tue.Value = DateTime.Parse("12:00");
			week[1] = true;
		}
		private void Cb_wed_Checked(object sender, RoutedEventArgs e)
		{
			tp_wed.IsEnabled = true;
			tp_wed.Value = DateTime.Parse("12:00");
			week[2] = true;
		}
		private void Cb_thu_Checked(object sender, RoutedEventArgs e)
		{
			tp_thu.IsEnabled = true;
			tp_thu.Value = DateTime.Parse("12:00");
			week[3] = true;
		}
		private void Cb_fri_Checked(object sender, RoutedEventArgs e)
		{
			tp_fri.IsEnabled = true;
			tp_fri.Value = DateTime.Parse("12:00");
			week[4] = true;
		}
		private void Cb_sat_Checked(object sender, RoutedEventArgs e)
		{
			tp_sat.IsEnabled = true;
			tp_sat.Value = DateTime.Parse("12:00");
			week[5] = true;
		}
		private void Cb_sun_Checked(object sender, RoutedEventArgs e)
		{
			tp_sun.IsEnabled = true;
			tp_sun.Value = DateTime.Parse("12:00");
			week[6] = true;
		}
		private void Cb_mon_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_mon.IsEnabled = false;
			tp_mon.Value = null;
			week[0] = false;
		}
		private void Cb_tue_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_tue.IsEnabled = false;
			tp_tue.Value = null;
			week[1] = false;
		}
		private void Cb_wed_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_wed.IsEnabled = false;
			tp_wed.Value = null;
			week[2] = false;
		}
		private void Cb_thu_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_thu.IsEnabled = false;
			tp_thu.Value = null;
			week[3] = false;
		}
		private void Cb_fri_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_fri.IsEnabled = false;
			tp_fri.Value = null;
			week[4] = false;
		}
		private void Cb_sat_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_sat.IsEnabled = false;
			tp_sat.Value = null;
			week[5] = false;
		}
		private void Cb_sun_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_sun.IsEnabled = false;
			tp_sun.Value = null;
			week[6] = false;
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
			XrcsList_win win = new XrcsList_win(super, exerciseList);
			win.Show();
		}
	}
}
