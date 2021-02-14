using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Student.BLL.Models;

namespace Student.BLL.Interfsces
{
    public interface IStudentService
    {
        public Task<StudentModel> AddStudent(StudentModel student);
        public Task<List<StudentModel>> GetStudents();
        public Task<StudentModel> FindById(int id);
        public Task<List<StudentModel>> FindByGroupName(string name);
        public Task<List<StudentModel>> FindByUniversity(string name);
        public Task<StudentModel> DeleteStudent(int id);
        public Task<StudentModel> UpdateStudent(StudentModel student);
    }
}
