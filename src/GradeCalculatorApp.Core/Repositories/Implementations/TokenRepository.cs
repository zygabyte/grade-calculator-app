using System;
using System.Linq;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Data;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Repositories.Implementations
{
    public class TokenRepository : ITokenRepository, IDisposable
    {
        private readonly GradeCalculatorContext _gradeCalculatorContext;
        
        public TokenRepository(GradeCalculatorContext gradeCalculatorContext) => _gradeCalculatorContext = gradeCalculatorContext;
        
        public TokenUserMap ReadUserByTokenMap(string tokenMap)
        {
            try
            {
                return _gradeCalculatorContext.TokenUserMaps.FirstOrDefault(x => !x.IsDeleted && x.Token == tokenMap);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void Dispose()
        {
            _gradeCalculatorContext?.Dispose();
        }
    }
}