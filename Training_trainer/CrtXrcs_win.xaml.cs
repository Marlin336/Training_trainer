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
	public class Node<T>
	{
		public List<Node<T>> Children { get; set; }
		public bool IsChecked { get; set; }
		public T Value { get; set; }
		public Node(T value)
		{
			Children = new List<Node<T>>();
			this.Value = value;
		}

		public Node()
		{
			Children = new List<Node<T>>();
		}

		public void AddChild(Node<T> child)
		{
			Children.Add(child);
		}
	}
	public class Tree<T>
	{
		public Node<T> Root { get; set; }
	}

	/// <summary>
	/// Логика взаимодействия для CrtXrcs_win.xaml
	/// </summary>
	public partial class CrtXrcs_win : Window
	{
		public CrtXrcs_win()
		{
			InitializeComponent();
		}
	}
}
