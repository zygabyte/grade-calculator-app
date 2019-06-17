namespace GradeCalculatorApp.Data.Models
{
    public class RegisteredCourseGradeModel
    {
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
        public string Grade { get; set; }
    }
}