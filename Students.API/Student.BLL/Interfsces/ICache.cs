using Student.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Student.BLL.Interfsces
{
    public interface ICache<T> where T : class
    {
        public T Get(int key);
        public void Delete(int key);
        public void Add(int key, T value);
        public int Count { get; }

    }
}
