using GradeCalculatorApp.EnumLibrary;

namespace GradeCalculatorApp.Data.Domains
{
    public class RegisteredCourseGrade : BaseEntity
    {
        public RegisteredCourse RegisteredCourse { get; set; }
        public long RegisteredCourseId { get; set; }
        public int Attendance { get; set; }
        public int Quiz1 { get; set; }
        public int Quiz2 { get; set; }
        public int Assignment1 { get; set; }
        public int Assignment2 { get; set; }
        public int MidSemester { get; set; }
        public int Project { get; set; }
        public int Exam { get; set; }
        public int TotalCa { get; set; }
        public int FinalScore { get; set; }
        public Grade Grade { get; set; }
    }
}