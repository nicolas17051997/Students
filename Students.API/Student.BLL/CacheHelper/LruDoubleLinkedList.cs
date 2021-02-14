using System;
using System.Collections.Generic;
using System.Text;

namespace Student.BLL.CacheHelper
{
	public class LruDoubleLinkedList<T> where T: class
	{
		private LruNode<T> Head;
		private LruNode<T> Tail;

		public LruDoubleLinkedList()
		{
			Head = new LruNode<T>();
			Tail = new LruNode<T>();
			Head.Next = Tail;
			Tail.Previous = Head;
		}

		public void AddToTop(LruNode<T> node)
		{
			node.Next = Head.Next;
			Head.Next.Previous = node;
			node.Previous = Head;
			Head.Next = node;
		}

		public void RemoveNode(LruNode<T> node)
		{
			node.Previous.Next = node.Next;
			node.Next.Previous = node.Previous;
			node.Next = null;
			node.Previous = null;
		}

		public LruNode<T> RemoveLRUNode()
		{
			LruNode<T> target = Tail.Previous;
			RemoveNode(target);
			return target;
		}
	}
}
