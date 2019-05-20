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
	/// Логика взаимодействия для Profile_win.xaml
	/// </summary>
	public partial class Profile_win : Window
	{
		public Profile_win()
		{
			InitializeComponent();
		}

		private void B_edit_Click(object sender, RoutedEventArgs e)
		{
			Passreq win = new Passreq();
			win.Show();
		}
	}
}
