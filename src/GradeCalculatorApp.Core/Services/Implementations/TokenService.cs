using System;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ILecturerRepository _lecturerRepository;
        
        public TokenService(ITokenRepository tokenRepository, IStudentRepository studentRepository, ILecturerRepository lecturerRepository)
        {
            _tokenRepository = tokenRepository;
            _studentRepository = studentRepository;
            _lecturerRepository = lecturerRepository;
        }
        
        public TokenUserMap ReadUserByTokenMap(string tokenMap)
        {
            try
            {
                return _tokenRepository.ReadUserByTokenMap(tokenMap);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Student ReadStudentByEmail(string email)
        {
            try
            {
                return _studentRepository.ReadStudentByEmail(email);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Lecturer ReadLecturerByEmail(string email)
        {
            try
            {
                return _lecturerRepository.ReadLecturerByEmail(email);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}