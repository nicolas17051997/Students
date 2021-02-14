using Student.BLL.CacheHelper;
using Student.BLL.Interfsces;
using Student.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Student.BLL.Services
{
    public class LruCache<T> : ICache<T> where T: StudentModel
    {
        private int capacity;
        private int count;
        Dictionary<int, LruNode<T>> map;
        LruDoubleLinkedList<T> doubleLinkedList;

        public int Count => this.count;

        public LruCache(int capacity)
        {
            this.capacity = capacity;
            this.count = 0;
            map = new Dictionary<int, LruNode<T>>();
            doubleLinkedList = new LruDoubleLinkedList<T>();
        }

        
        public T Get(int key)
        {
            if (!map.ContainsKey(key)) return null;
            LruNode<T> node = map[key];
            doubleLinkedList.RemoveNode(node);
            doubleLinkedList.AddToTop(node);
            return node.Value;
        }

        public void Add(int key, T value)
        {
            
            if (map.ContainsKey(key))
            {
                LruNode<T> node = map[key];
                doubleLinkedList.RemoveNode(node);
                node.Value = value;
                doubleLinkedList.AddToTop(node);
            }
            else
            {
                
                if (count == capacity)
                {
                    LruNode<T> lru = doubleLinkedList.RemoveLRUNode();
                    map.Remove(lru.Key);
                    count--;
                }

                LruNode<T> node = new LruNode<T>(key, value);
                doubleLinkedList.AddToTop(node);
                map[key] = node;
                count++;
            }

        }

        public void Delete(int key)
        {
            if (map.ContainsKey(key))
            {
                LruNode<T> node = map[key];
                doubleLinkedList.RemoveNode(node);
            }
            
        }
    }
}
