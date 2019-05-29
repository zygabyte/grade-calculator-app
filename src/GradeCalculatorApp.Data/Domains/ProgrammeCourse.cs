using System.Collections.Generic;

namespace GradeCalculatorApp.Data.Domains
{
    public class ProgrammeCourse : BaseEntity
    {
        public Programme Programme { get; set; }
        public long ProgrammeId { get; set; }
        public List<Course> Courses { get; set; }
    }
}