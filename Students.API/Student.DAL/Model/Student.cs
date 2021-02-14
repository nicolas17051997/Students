using System;
using System.Collections.Generic;
using System.Text;

namespace Student.DAL.Model
{
   public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string University { get; set; }
        public string Group { get; set; }
    }
}
