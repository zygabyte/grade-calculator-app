namespace GradeCalculatorApp.Data.Domains
{
    public class RegisteredCourse : BaseEntity
    {
        public Student Student { get; set; }
        public long StudentId { get; set; }
        public Lecturer Lecturer { get; set; }
        public long LecturerId { get; set; }
        public Course Course { get; set; }
        public long CourseId { get; set; }
    }
}