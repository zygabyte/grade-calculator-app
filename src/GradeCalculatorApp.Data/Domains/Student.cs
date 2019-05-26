namespace GradeCalculatorApp.Data.Domains
{
    public class Student : User
    {
        public string MatricNumber { get; set; }
        public Programme Programme { get; set; }
        public long ProgrammeId { get; set; }
    }
}