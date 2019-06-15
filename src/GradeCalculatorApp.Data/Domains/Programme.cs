using System.Collections.Generic;

namespace GradeCalculatorApp.Data.Domains
{
    public class Programme : Unit
    {
        public Department Department { get; set; }
        public long DepartmentId { get; set; }
        public List<ProgrammeCourse> ProgrammeCourses { get; set; }
        public List<Student> Students { get; set; }
    }
}