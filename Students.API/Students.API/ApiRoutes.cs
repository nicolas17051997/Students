using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.API
{
    public static class ApiRoutes
    {
        public const string Base = "api/";
        public static class Student
        {
            public const string Students = Base + "students";
            public const string Create = Base + "create";
            public const string GetById = Base + "student/{studentId:int}";
            public const string GetByGroupName = Base + "student/{groupName:string}";
            public const string GetByUniversity = Base + "student/{university:string}";
            public const string Delete = Base + "student/delete/{id}";
            public const string Update = Base + "student/update";

        }
    }
}
