using System.Collections.Generic;
using System.Threading.Tasks;

namespace Student.DAL.Interfaces
{
   public interface ISaveStrategy<T> where T: Student.DAL.Model.Student
    {
        public Task<bool> AddNewStudent(T value);
        public Task<T> UpdateValue(T entity);
        public Task<List<T>> GetAll();
        public Task<T> GetById(int id);
        public Task<List<T>> GetByUniversity(string university);
        public Task<List<T>> GetByGroupName(string groupName);
        public Task<T> Delete(int id);
    }
}
