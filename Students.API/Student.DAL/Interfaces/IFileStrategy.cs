using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Student.DAL.Interfaces
{
    public interface IFileStrategy<T> where T : class
    {
        public string PathDirectory { get; }
        public Task<bool> WriteToFileAsync(T data);
        public Task<bool> WriteToFileCollectionAsync(List<T> data);
        public Task<bool> WriteValue();
        public Task<List<T>> ReadFromFileAsync();
    }
}
