namespace GradeCalculatorApp.Data.Domains
{
    public class ProgrammeCourse : BaseEntity
    {
        public Programme Programme { get; set; }
        public long ProgrammeId { get; set; }
        public Course Course { get; set; }
        public long CourseId { get; set; }
    }
}