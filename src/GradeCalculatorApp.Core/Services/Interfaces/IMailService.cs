namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface IMailService
    {
        void SendRegisterMail(string toEmail, string fullName, string userRole, string tokenMap);
    }
}