using System.Collections.Generic;

namespace GradeCalculatorApp.Data.Domains
{
    public class SessionCourse : BaseEntity
    {
        public Session Session { get; set; }
        public long SessionId { get; set; }
        public List<Course> Courses { get; set; }
    }
}