using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface ITokenService
    {
        TokenUserMap ReadUserByTokenMap(string tokenMap);
        Student ReadStudentByEmail(string email);
        Lecturer ReadLecturerByEmail(string email);
    }
}