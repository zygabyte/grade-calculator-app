namespace GradeCalculatorApp.Data.Models
{
    public class RegisteredCourseModel
    {
        public long Id { get; set; }
        public string Student { get; set; }
        public long StudentId { get; set; }
        public string Lecturer { get; set; }
        public long LecturerId { get; set; }
        public string Course { get; set; }
        public string CourseCode { get; set; }
        public int CourseCredit { get; set; }
        public long CourseId { get; set; }
        public string SessionSemester { get; set; }
        public long SessionSemesterId { get; set; }
    }
}