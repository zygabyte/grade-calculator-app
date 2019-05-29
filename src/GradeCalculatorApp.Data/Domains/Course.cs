namespace GradeCalculatorApp.Data.Domains
{
    public class Course : BaseEntity
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public int CreditUnit { get; set; }
    }
}