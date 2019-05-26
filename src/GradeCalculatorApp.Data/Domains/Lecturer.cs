using System.Collections.Generic;

namespace GradeCalculatorApp.Data.Domains
{
    public class Lecturer : User
    {
        public Department Department { get; set; }
        public long DepartmentId { get; set; }
        public List<Course> Courses { get; set; }
    }
}