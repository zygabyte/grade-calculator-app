using System;
using System.Collections.Generic;
using System.Linq;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Data;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Repositories.Implementations
{
    public class RegisteredCourseRepository : IRegisteredCourseRepository, IDisposable
    {
        private readonly GradeCalculatorContext _gradeCalculatorContext;
        
        public RegisteredCourseRepository(GradeCalculatorContext gradeCalculatorContext) => _gradeCalculatorContext = gradeCalculatorContext;
        
        public bool CreateRegisteredCourses(List<RegisteredCourse> registeredCourses)
        {
            try
            {
                registeredCourses.ForEach(registeredCourse => _gradeCalculatorContext.RegisteredCourses.Add(registeredCourse));

                return _gradeCalculatorContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<RegisteredCourseModel> ReadRegisteredCourses(long sessionSemesterId, long lecturerId)
        {
            try
            {
                return _gradeCalculatorContext.RegisteredCourses
                    .Include(x => x.Course)
                    .Include(x => x.Student).Include(x => x.Lecturer)
                    .Where(x => !x.IsDeleted && !x.Course.IsDeleted && !x.Lecturer.IsDeleted && !x.Student.IsDeleted
                                && x.SessionSemesterId == sessionSemesterId && x.LecturerId == lecturerId)
                    .Select(x => new RegisteredCourseModel
                    {
                        Id = x.Id, Course = x.Course.Name, Student = $"{x.Student.FirstName} {x.Student.LastName}",
                        Lecturer = $"{x.Lecturer.FirstName} {x.Lecturer.LastName}", CourseId = x.CourseId, StudentId = x.StudentId,
                        LecturerId = x.LecturerId, SessionSemesterId = x.SessionSemesterId, CourseCode = x.Course.Code, CourseCredit = x.Course.CreditUnit
                    });
            }
            catch (Exception e)
            {
                return new List<RegisteredCourseModel>();
            }
        }

        public IEnumerable<RegisteredCourseModel> ReadRegisteredCoursesByStudent(long sessionSemesterId, long studentId)
        {
            try
            {
                return _gradeCalculatorContext.RegisteredCourses
                    .Include(x => x.Course)
                    .Include(x => x.Student).Include(x => x.Lecturer)
                    .Where(x => !x.IsDeleted && !x.Course.IsDeleted && !x.Lecturer.IsDeleted  && !x.Student.IsDeleted 
                                && x.SessionSemesterId == sessionSemesterId && x.StudentId == studentId)
                    .Select(x => new RegisteredCourseModel
                    {
                        Id = x.Id, Course = x.Course.Name, Student = $"{x.Student.FirstName} {x.Student.LastName}",
                        Lecturer = $"{x.Lecturer.FirstName} {x.Lecturer.LastName}", CourseId = x.CourseId, StudentId = x.StudentId,
                        LecturerId = x.LecturerId, SessionSemesterId = x.SessionSemesterId, CourseCode = x.Course.Code, CourseCredit = x.Course.CreditUnit
                    });
            }
            catch (Exception e)
            {
                return new List<RegisteredCourseModel>();
            }
        }

        public void Dispose()
        {
            _gradeCalculatorContext?.Dispose();
        }
    }
}