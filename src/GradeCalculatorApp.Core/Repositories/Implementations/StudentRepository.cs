using System;
using System.Collections.Generic;
using System.Linq;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Data;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Repositories.Implementations
{
    public class StudentRepository : IStudentRepository, IDisposable
    {
        private readonly GradeCalculatorContext _gradeCalculatorContext;
        
        public StudentRepository(GradeCalculatorContext gradeCalculatorContext) => _gradeCalculatorContext = gradeCalculatorContext;
        
        public bool CreateStudent(Student student, string tokenMap)
        {
            try
            {
                _gradeCalculatorContext.Students.Add(student);
                _gradeCalculatorContext.TokenUserMaps.Add(new TokenUserMap
                {
                    Email = student.Email, UserRole = student.UserRole, Token = tokenMap
                });

                return _gradeCalculatorContext.SaveChanges() > 0;
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
                return takeAll 
                    ? _gradeCalculatorContext.Students
                        .Include(x => x.Programme)
                        .Where(x => !x.IsDeleted && x.IsActive) 
                    : _gradeCalculatorContext.Students
                        .Include(x => x.Programme)
                        .Where(x => !x.IsDeleted && x.IsActive).Take(count);
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
                return _gradeCalculatorContext.Students
                    .Include(x => x.Programme)
                    .FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == studentId && !x.Programme.IsDeleted);
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
                return _gradeCalculatorContext.Students
                    .Include(x => x.Programme)
                    .FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Email == email && !x.Programme.IsDeleted);
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
                var student = _gradeCalculatorContext.Students.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == studentId);

                if (student == null) return false;
                
                student.IsDeleted = true;
                student.Modified = DateTime.Now;

                _gradeCalculatorContext.Entry(student).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;

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
                var currentStudent = _gradeCalculatorContext.Students.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == studentId);

                if (currentStudent == null) return false;
                
                currentStudent.Email = student.Email;
                currentStudent.LastName = student.LastName;
                currentStudent.FirstName = student.FirstName;
                currentStudent.UserRole = student.UserRole;
                currentStudent.ProgrammeId = student.ProgrammeId;
                currentStudent.MatricNumber = student.MatricNumber;
                currentStudent.PasswordHash = student.PasswordHash;
                currentStudent.Modified = DateTime.Now;
                    
                _gradeCalculatorContext.Entry(currentStudent).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
        public int CountTotalStudents()
        {
            try
            {
                return _gradeCalculatorContext.Students.Count(x => !x.IsDeleted);
            }
            catch (Exception e)
            {
                return default;
            }
        }

        public void Dispose()
        {
            _gradeCalculatorContext?.Dispose();
        }
    }
}