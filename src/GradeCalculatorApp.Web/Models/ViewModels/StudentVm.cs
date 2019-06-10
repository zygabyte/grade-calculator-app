namespace GradeCalculatorApp.Web.Models.ViewModels
{
    public class StudentVm
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MatricNumber { get; set; }
        public string Programme { get; set; }
        public long ProgrammeId { get; set; }
    }
}