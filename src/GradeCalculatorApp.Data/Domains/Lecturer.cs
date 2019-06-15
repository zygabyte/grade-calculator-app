using System.Collections.Generic;
using GradeCalculatorApp.EnumLibrary;

namespace GradeCalculatorApp.Data.Domains
{
    public class Lecturer : User
    {
        public Department Department { get; set; }
        public long DepartmentId { get; set; }
        public List<LecturerCourse> LecturerCourses { get; set; }
        public List<RegisteredCourse> RegisteredCourses { get; set; }

        public Lecturer()
        {
            UserRole = UserRole.Lecturer;
        }
    }
}