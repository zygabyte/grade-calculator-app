using System;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.EnumLibrary;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly ILecturerRepository _lecturerRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IHashService _hashService;
        
        public AccountService(ILecturerRepository lecturerRepository, IStudentRepository studentRepository, IHashService hashService)
        {
            _lecturerRepository = lecturerRepository;
            _studentRepository = studentRepository;
            _hashService = hashService;
        }

        public bool LogIn(string email, string password, UserRole userRole)
        {
            try
            {
                string passwordHash;
                switch (userRole)
                {
                    case UserRole.Student:
                        passwordHash = _studentRepository.ReadStudentByEmail(email)?.PasswordHash;
                        break;
                    
                    case UserRole.Lecturer:
                        passwordHash = _lecturerRepository.ReadLecturerByEmail(email)?.PasswordHash;
                        break;
                    
                    default: return false;
                }
                
                return !string.IsNullOrEmpty(passwordHash) && _hashService.ValidatePassword(password, passwordHash);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Register(User user)
        {
            try
            {
                switch (user.UserRole)
                {
                    case UserRole.Student:
                        var student = _studentRepository.ReadStudentByEmail(user.Email);
                        if (student != null)
                        {
                            student.FirstName = user.FirstName;
                            student.LastName = user.LastName;
                            student.PasswordHash = _hashService.HashPassword(user.PasswordHash);
                            return _studentRepository.UpdateStudent(student.Id, student);
                        }
                        
                        break;
                    
                    case UserRole.Lecturer:
                        var lecturer = _lecturerRepository.ReadLecturerByEmail(user.Email);
                        if (lecturer != null)
                        {
                            lecturer.FirstName = user.FirstName;
                            lecturer.LastName = user.LastName;
                            lecturer.PasswordHash = _hashService.HashPassword(user.PasswordHash);
                            return _lecturerRepository.UpdateLecturer(lecturer.Id, lecturer);
                        }

                        break;
                    
                    default: return false;
                }

                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}