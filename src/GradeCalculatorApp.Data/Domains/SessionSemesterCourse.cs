namespace GradeCalculatorApp.Data.Domains
{
    public class SessionSemesterCourse : BaseEntity
    {
        public SessionSemester SessionSemester { get; set; }
        public long SessionSemesterId { get; set; }
        public Course Course { get; set; }
        public long CourseId { get; set; }
    }
}