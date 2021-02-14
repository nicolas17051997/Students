using Student.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Student.BLL.CacheHelper
{
	public class LruNode<T> where T: class
	{
		public int Key { get; set; }
		public T Value { get; set; }
		public LruNode<T> Previous { get; set; }
		public LruNode<T> Next { get; set; }
		public LruNode() { }
		public LruNode(int k, T v)
		{
			this.Key = k;
			this.Value = v;
		}
	}
}
