using System;
using System.Collections.Generic;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class StudentService : IStudentService
    {

        private readonly IStudentRepository _studentRepository;
        private readonly IMailService _mailService;
        
        public StudentService(IStudentRepository studentRepository, IMailService mailService)
        {
            _studentRepository = studentRepository;
            _mailService = mailService;
        }

        public bool CreateStudent(Student student)
        {
            try
            {
                if (student != null && _studentRepository.CreateStudent(student))
                {
                    _mailService.SendRegisterMail(student.Email, $"{student.FirstName} {student.LastName}", student.UserRole.ToString());
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<Student> ReadStudents(bool takeAll = true, int count = 1000)
        {
            try
            {
                return _studentRepository.ReadStudents(takeAll, count);
            }
            catch (Exception e)
            {
                return new List<Student>();
            }
        }

        public Student ReadStudent(long studentId)
        {
            try
            {
                return studentId > 0 ? _studentRepository.ReadStudent(studentId) : null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool DeleteStudent(long studentId)
        {
            try
            {
                return studentId > 0 && _studentRepository.DeleteStudent(studentId);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateStudent(long studentId, Student student)
        {
            try
            {
                return studentId > 0 && student != null && _studentRepository.UpdateStudent(studentId, student);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}