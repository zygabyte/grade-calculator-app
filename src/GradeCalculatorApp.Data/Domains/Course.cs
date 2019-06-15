using System.Collections.Generic;

namespace GradeCalculatorApp.Data.Domains
{
    public class Course : Unit
    {
        public int CreditUnit { get; set; }
        public List<SessionSemesterCourse> SessionSemesterCourses { get; set; }
        public List<LecturerCourse> LecturerCourses { get; set; }
        public List<ProgrammeCourse> ProgrammeCourses { get; set; }
        public List<RegisteredCourse> RegisteredCourses { get; set; }
    }
}