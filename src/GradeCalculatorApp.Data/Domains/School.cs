using System.Collections.Generic;

namespace GradeCalculatorApp.Data.Domains
{
    public class School : Unit
    {
        public List<Department> Departments { get; set; }
    }
}