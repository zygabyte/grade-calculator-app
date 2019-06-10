namespace GradeCalculatorApp.Web.Models.ViewModels
{
    public class LecturerVm
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public long DepartmentId { get; set; }
    }
}