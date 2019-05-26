using System.Collections.Generic;

namespace GradeCalculatorApp.Data.Domains
{
    public class Department : BaseEntity 
    {
        public string Name { get; set; }
        public School School { get; set; }
        public long SchoolId { get; set; }
        public List<Programme> Programmes { get; set; }
        public List<Lecturer> Lecturers { get; set; }
    }
}