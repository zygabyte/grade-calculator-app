using GradeCalculatorApp.EnumLibrary;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface IAccountService
    {
        bool LogIn(string email, string password, UserRole userRole);
        bool Register(string email, string password, UserRole userRole);
    }
}