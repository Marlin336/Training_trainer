﻿using System;
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
    /// Логика взаимодействия для Passreq.xaml
    /// </summary>
    public partial class Passreq : Window
    {
        public Passreq()
        {
            InitializeComponent();
        }

		private void B_accept_Click(object sender, RoutedEventArgs e)
		{
			if (tb_pass.Password == "0000")
			{
				Profedit_win win = new Profedit_win();
				Close();
				win.ShowDialog();
			}
		}
	}
}