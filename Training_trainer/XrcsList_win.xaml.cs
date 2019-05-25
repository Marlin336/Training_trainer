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
	/// Логика взаимодействия для XrcsList_win.xaml
	/// </summary>
	public partial class XrcsList_win : Window
	{
		Main_win super { get; }
		public XrcsList_win(Main_win super, List<int> exerc_list)//Список передаётся для отметок уже добавленных упражнений
		{
			InitializeComponent();
			this.super = super;
		}

		private void B_crt_exerc_Click(object sender, RoutedEventArgs e)
		{
			CrtXrcs_win win = new CrtXrcs_win(super);
			win.Show();
		}
	}
}
