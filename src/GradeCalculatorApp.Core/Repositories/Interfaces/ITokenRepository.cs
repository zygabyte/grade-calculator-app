using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        TokenUserMap ReadUserByTokenMap(string tokenMap);
    }
}