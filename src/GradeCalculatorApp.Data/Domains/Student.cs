using System.Collections.Generic;
using GradeCalculatorApp.EnumLibrary;

namespace GradeCalculatorApp.Data.Domains
{
    public class Student : User
    {
        public string MatricNumber { get; set; }
        public Programme Programme { get; set; }
        public long ProgrammeId { get; set; }
        public List<RegisteredCourse> RegisteredCourses { get; set; }

        public Student()
        {
            UserRole = UserRole.Student;
        }
    }
}