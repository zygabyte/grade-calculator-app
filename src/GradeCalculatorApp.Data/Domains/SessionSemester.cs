using System;
using System.Collections.Generic;

namespace GradeCalculatorApp.Data.Domains
{
    public class SessionSemester : BaseEntity
    {
        public Semester Semester { get; set; }
        public long SemesterId { get; set; }
        public Session Session { get; set; }
        public long SessionId { get; set; }
        public DateTime SemesterStartDate { get; set; }
        public DateTime SemesterEndDate { get; set; }
        public bool IsCurrent { get; set; }
        public List<SessionSemesterCourse> SessionSemesterCourses { get; set; }
        public List<RegisteredCourse> RegisteredCourses { get; set; }
    }
}