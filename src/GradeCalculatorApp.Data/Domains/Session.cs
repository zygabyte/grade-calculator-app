using System;
using System.Collections.Generic;

namespace GradeCalculatorApp.Data.Domains
{
    public class Session : BaseEntity
    {
        public string Name { get; set; }
        public Semester Semester { get; set; }
        public long SemesterId { get; set; }
        public List<Course> Courses { get; set; }
        public DateTime SemesterStartDate { get; set; }
        public DateTime SemesterEndDate { get; set; }
    }
}