using System.Collections.Generic;

namespace GradeCalculatorApp.Data.Domains
{
    public class LecturerCourse : BaseEntity
    {
        public Lecturer Lecturer { get; set; }
        public long LecturerId { get; set; }
        public List<Course> Courses { get; set; }
    }
}