namespace GradeCalculatorApp.Data.Domains
{
    public class Course : BaseEntity
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public int CreditUnit { get; set; }
        public Programme Programme { get; set; }
        public long ProgrammeId { get; set; }
        public Session Session { get; set; }
        public long SessionId { get; set; }
        public Lecturer Lecturer { get; set; }
        public long LecturerId { get; set; }
    }
}