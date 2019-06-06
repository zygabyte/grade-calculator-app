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
        
        public bool CreateStudent(Student student)
        {
            try
            {
                _gradeCalculatorContext.Students.Add(student);

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
                    ? _gradeCalculatorContext.Students.Where(x => !x.IsDeleted && x.IsActive) 
                    : _gradeCalculatorContext.Students.Where(x => !x.IsDeleted && x.IsActive).Take(count);
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
                return _gradeCalculatorContext.Students.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == studentId);
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
                currentStudent.Programme = student.Programme;
                currentStudent.ProgrammeId = student.ProgrammeId;
                currentStudent.MatricNumber = student.MatricNumber;
                currentStudent.Modified = DateTime.Now;
                    
                _gradeCalculatorContext.Entry(currentStudent).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void Dispose()
        {
            _gradeCalculatorContext?.Dispose();
        }
    }
}