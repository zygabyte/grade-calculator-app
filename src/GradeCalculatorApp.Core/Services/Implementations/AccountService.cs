using System;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.EnumLibrary;
using Microsoft.AspNetCore.Http;
using SessionExtensions = GradeCalculatorApp.Core.Utilities.SessionExtensions;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly ILecturerRepository _lecturerRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IAdministratorRepository _administratorRepository;
        private readonly IHashService _hashService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public AccountService(ILecturerRepository lecturerRepository, IStudentRepository studentRepository, IAdministratorRepository administratorRepository, 
            IHashService hashService, IHttpContextAccessor httpContextAccessor)
        {
            _lecturerRepository = lecturerRepository;
            _studentRepository = studentRepository;
            _administratorRepository = administratorRepository;
            _hashService = hashService;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool LogIn(string email, string password, UserRole userRole)
        {
            try
            {
                string passwordHash;
                User user;
                switch (userRole)
                {
                    case UserRole.Student:
                        user = _studentRepository.ReadStudentByEmail(email);
                        passwordHash = user?.PasswordHash;
                        break;
                    
                    case UserRole.Lecturer:
                        user = _lecturerRepository.ReadLecturerByEmail(email);
                        passwordHash = user?.PasswordHash;
                        break;
                    
                    case UserRole.Administrator:
                        user = _administratorRepository.ReadAdministratorByEmail(email);
                        passwordHash = user?.PasswordHash;
                        break;
                    
                    default: return false;
                }

                var status = !string.IsNullOrEmpty(passwordHash) &&
                             _hashService.ValidatePassword(password, passwordHash);
                
                if (status) SetUserInSession(new User
                {
                    Id = user.Id, Email = user.Email, UserRole = user.UserRole, 
                    FirstName = user.FirstName, LastName = user.LastName, PasswordHash = user.PasswordHash
                });
                return status;
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
                    
                    case UserRole.Administrator:
                        user.PasswordHash = _hashService.HashPassword(user.PasswordHash);
                        return _administratorRepository.CreateAdministrator(new Administrator
                        {
                            Email = user.Email, FirstName = user.FirstName, LastName = user.LastName,
                            UserRole = user.UserRole, PasswordHash = user.PasswordHash
                        });
                    
                    default: return false;
                }

                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void SetUserInSession(User user)
        {
            try
            {
                SessionExtensions.Set(_httpContextAccessor.HttpContext.Session, "User", user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public User GetUserInSession()
        {
            try
            {
                return SessionExtensions.Get<User>(_httpContextAccessor.HttpContext.Session, "User");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool IsUserSessionActive()
        {
            try
            {
                return SessionExtensions.Get<User>(_httpContextAccessor.HttpContext.Session, "User").Email != null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public void ClearSession()
        {
            try
            {
                SessionExtensions.ClearSession(_httpContextAccessor.HttpContext.Session);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        
    }
}