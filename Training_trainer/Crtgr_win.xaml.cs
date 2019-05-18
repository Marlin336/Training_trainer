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
        public Crtgr_win()
        {
            InitializeComponent();
        }

		private void Cb_mon_Checked(object sender, RoutedEventArgs e)
		{
			tp_mon.IsEnabled = true;
		}

		private void Cb_tue_Checked(object sender, RoutedEventArgs e)
		{
			tp_tue.IsEnabled = true;
		}

		private void Cb_wed_Checked(object sender, RoutedEventArgs e)
		{
			tp_wed.IsEnabled = true;
		}

		private void Cb_thu_Checked(object sender, RoutedEventArgs e)
		{
			tp_thu.IsEnabled = true;
		}

		private void Cb_fri_Checked(object sender, RoutedEventArgs e)
		{
			tp_fri.IsEnabled = true;
		}

		private void Cb_sat_Checked(object sender, RoutedEventArgs e)
		{
			tp_sat.IsEnabled = true;
		}

		private void Cb_sun_Checked(object sender, RoutedEventArgs e)
		{
			tp_sun.IsEnabled = true;
		}

		private void Cb_mon_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_mon.IsEnabled = false;
			tp_mon.Value = DateTime.Parse("12:00");
		}

		private void Cb_tue_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_tue.IsEnabled = false;
			tp_tue.Value = DateTime.Parse("12:00");
		}

		private void Cb_wed_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_wed.IsEnabled = false;
			tp_wed.Value = DateTime.Parse("12:00");
		}

		private void Cb_thu_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_thu.IsEnabled = false;
			tp_thu.Value = DateTime.Parse("12:00");
		}

		private void Cb_fri_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_fri.IsEnabled = false;
			tp_fri.Value = DateTime.Parse("12:00");
		}

		private void Cb_sat_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_sat.IsEnabled = false;
			tp_sat.Value = DateTime.Parse("12:00");
		}

		private void Cb_sun_Unchecked(object sender, RoutedEventArgs e)
		{
			tp_sun.IsEnabled = false;
			tp_sun.Value = DateTime.Parse("12:00");
		}
	}
}
