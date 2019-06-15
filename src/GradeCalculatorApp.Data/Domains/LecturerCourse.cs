namespace GradeCalculatorApp.Data.Domains
{
    public class LecturerCourse : BaseEntity
    {
        public Lecturer Lecturer { get; set; }
        public long LecturerId { get; set; }
        public Course Course { get; set; }
        public long CourseId { get; set; }
    }
}