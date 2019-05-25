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
		private class XTreeViewItem : TreeViewItem
		{
			public int id { get; }
			public XTreeViewItem(int id, string header)
			{
				this.id = id;
				Header = header;
			}
		}
		private class XCheckBox : CheckBox
		{
			public int id { get; }
			public XCheckBox(int id, string content)
			{
				this.id = id;
				Content = content;
			}
		}
		Main_win super { get; }
		public CrtXrcs_win(Main_win super)
		{
			InitializeComponent();
			this.super = super;
			NpgsqlCommand comm = new NpgsqlCommand("select group_id, group_name from muscle_view group by group_id, group_name order by group_id", super.conn);
			try
			{
				super.conn.Open();
				NpgsqlDataReader reader = comm.ExecuteReader();
				List<XTreeViewItem> list = new List<XTreeViewItem>();
				for (int i = 0; reader.Read(); i++)
				{
					XTreeViewItem item = new XTreeViewItem(reader.GetInt32(0), reader.GetString(1));
					list.Add(item);
				}
				for (int i = 0; i < list.Count; i++)
				{
					string sql = "select muscle_id, muscle_name from muscle_view where group_id = " + list[i].id + " group by muscle_id, muscle_name ";
					comm = new NpgsqlCommand(sql, super.conn);
					super.conn.Close();
					super.conn.Open();
					reader = comm.ExecuteReader();
					for (int j = 0; reader.Read() ; j++)
						list[i].Items.Add(new XCheckBox(reader.GetInt32(0), reader.GetString(1)));
					tv_main.Items.Add(list[i]);
				}
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
