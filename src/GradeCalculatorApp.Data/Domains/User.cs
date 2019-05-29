using GradeCalculatorApp.EnumLibrary.Enums;

namespace GradeCalculatorApp.Data.Domains
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserRole UserRole { get; set; }
    }
}