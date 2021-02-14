using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Student.BLL.Interfsces;
using Student.BLL.Models;
using Students.API.Responses;

namespace Students.API.Controllers
{
    [ApiController]
    public class StudentController : ControllerBase
    {
        IConfiguration configuration;
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService,
            IConfiguration configuration)
        {
            this._studentService = studentService;
            this.configuration = configuration;
        }

        [HttpGet(ApiRoutes.Student.GetById)]
        public async Task<StudentModel> GetStudent(int id)
        {
            var student = await _studentService.FindById(id);
            return student;
        }

        [HttpPost(ApiRoutes.Student.Create)]
        public async Task<ResponseResult> AddStudent([FromForm] Student.BLL.Models.StudentModel student)
        {
            var result = await _studentService.AddStudent(student);
            return new ResponseResult(result is null ? false : true);
        }

        [HttpGet(ApiRoutes.Student.Students)]
        public async Task<List<Student.BLL.Models.StudentModel>> GetAll()
        {
            var students = await _studentService.GetStudents();
            return students;
        }

        [HttpDelete(ApiRoutes.Student.Delete)]
        public async Task<ResponseResult> Delete(int id)
        {
            var result = await _studentService.DeleteStudent(id);

            if (result is null)
                return new ResponseResult(false);
            else
                return new ResponseResult(true);
        }

        [HttpPut(ApiRoutes.Student.Update)]
        public async Task<ResponseResult> UpdateStudent([FromForm] StudentModel student)
        {
            var result = await _studentService.UpdateStudent(student);

            if (result is null)
                return new ResponseResult(false);
            else
                return new ResponseResult(true);
        }

    }
}
