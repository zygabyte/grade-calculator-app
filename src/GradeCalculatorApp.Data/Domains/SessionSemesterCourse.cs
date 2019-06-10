using System.Collections.Generic;

namespace GradeCalculatorApp.Data.Domains
{
    public class SessionSemesterCourse : BaseEntity
    {
        public SessionSemester SessionSemester { get; set; }
        public long SessionSemesterId { get; set; }
        public List<Course> Courses { get; set; }
    }
}