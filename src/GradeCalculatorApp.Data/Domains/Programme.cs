using System.Collections.Generic;

namespace GradeCalculatorApp.Data.Domains
{
    public class Programme : BaseEntity
    {
        public string Name { get; set; }
        public Department Department { get; set; }
        public long DepartmentId { get; set; }
        public List<Course> Courses { get; set; }
    }
}