using Student.BLL.Interfsces;
using Student.BLL.Models;
using Student.DAL.EntityContext;
using Student.DAL.Interfaces;
using Student.DAL.Model;
using Student.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Student.BLL.Services
{
    public class StudentService : IStudentService
    {
        private ISaveStrategy<Student.DAL.Model.Student> _saveStrategy;
        private ICache<StudentModel> _memoryCache;

        public StudentService(StrategySaveType typeValue,
            ICache<StudentModel> memoryCache)
        {
            SaveType param = SaveType.json;
            if (typeValue == StrategySaveType.xml)
                param = SaveType.xml;

            _saveStrategy = new SystemReposytory<Student.DAL.Model.Student>(param);
            _memoryCache = memoryCache;
        }

        public async Task<StudentModel> AddStudent(StudentModel student)
        {
            var result = await _saveStrategy.AddNewStudent(new Student.DAL.Model.Student
            {
                Id = student.Id,
                FirstName = student.FirstName,
                Group = student.Group,
                LastName = student.LastName,
                University = student.University
            });

            if (result)
            {
                _memoryCache.Add(student.Id, student);
                return student;
            }
            else
                return null;
        }

        public async Task<StudentModel> DeleteStudent(int id)
        {
            var results = await _saveStrategy.Delete(id);
            await Task.Run(() => _memoryCache.Delete(id));
            if (results is null)
                return new StudentModel();
            else return new StudentModel
            {
                FirstName = results.FirstName,
                Group = results.Group,
                Id = results.Id,
                LastName = results.LastName,
                University = results.University,
            };
        }

        public async Task<List<StudentModel>> FindByGroupName(string name)
        {
            var result = new List<StudentModel>();
            var student = await _saveStrategy.GetByGroupName(name);
            student.ForEach(s => result.Add(new StudentModel
            {
                FirstName = s.FirstName,
                Group = s.Group,
                Id = s.Id,
                LastName = s.LastName,
                University = s.University,
            }));

            await Task.Run(() => result.ForEach(s => _memoryCache.Add(s.Id, s)));

            return result;
        }

        public async Task<StudentModel> FindById(int id)
        {
            StudentModel student = new StudentModel();
            if (_memoryCache.Count > 0)
                student = _memoryCache.Get(id);

            if (!(student is null))
                return student;

            var result = await _saveStrategy.GetById(id);
            return new StudentModel
            {
                FirstName = result.FirstName,
                Group = result.Group,
                Id = result.Id,
                LastName = result.LastName,
                University = result.University
            };
        }

        public async Task<List<StudentModel>> FindByUniversity(string name)
        {
            var result = new List<StudentModel>();
            var student = await _saveStrategy.GetByUniversity(name);
            student.ForEach(s => result.Add(new StudentModel
            {
                FirstName = s.FirstName,
                Group = s.Group,
                Id = s.Id,
                LastName = s.LastName,
                University = s.University,
            }));

            await Task.Run(() => result.ForEach(s => _memoryCache.Add(s.Id, s)));

            return result;
        }

        public async Task<List<StudentModel>> GetStudents()
        {
            var students = new List<StudentModel>();
            var student = await _saveStrategy.GetAll();
            student.ForEach(s => students.Add(new StudentModel
            {
                FirstName = s.FirstName,
                Group = s.Group,
                Id = s.Id,
                LastName = s.LastName,
                University = s.University,
            }));

            await Task.Run(() => students.ForEach(s => _memoryCache.Add(s.Id, s)));

            return students;
        }

        public async Task<StudentModel> UpdateStudent(StudentModel student)
        {
            var result = await _saveStrategy.UpdateValue(new DAL.Model.Student
            {
                University = student.University,
                FirstName = student.FirstName,
                Group = student.Group,
                Id = student.Id,
                LastName = student.LastName
            });
            if (result is null)
                return new StudentModel();
            else
            {
                await Task.Run(() => _memoryCache.Add(student.Id, student));
                return student;
            }

        }
    }
}
