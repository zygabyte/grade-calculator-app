using System.Collections.Generic;

namespace GradeCalculatorApp.Data.Domains
{
    public class School : BaseEntity
    {
        public string Name { get; set; }
        public List<Department> Departments { get; set; }
    }
}