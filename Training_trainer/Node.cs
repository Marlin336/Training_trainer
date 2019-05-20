using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training_trainer
{
    class Node<T>
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
}
