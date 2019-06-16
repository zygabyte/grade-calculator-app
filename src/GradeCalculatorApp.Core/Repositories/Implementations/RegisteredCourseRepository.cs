using System;
using System.Collections.Generic;
using System.Linq;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Data;
using GradeCalculatorApp.Data.Domains;
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

        public IEnumerable<RegisteredCourse> ReadRegisteredCourses(long sessionSemesterId, long lecturerId)
        {
            try
            {
                return _gradeCalculatorContext.RegisteredCourses
                        .Include(x => x.Course)
                        .Include(x => x.Student).Include(x => x.Lecturer)
                        .Where(x => !x.IsDeleted && !x.Course.IsDeleted && !x.Lecturer.IsDeleted && x.SessionSemesterId == sessionSemesterId && x.LecturerId == lecturerId);
            }
            catch (Exception e)
            {
                return new List<RegisteredCourse>();
            }
        }

        public void Dispose()
        {
            _gradeCalculatorContext?.Dispose();
        }
    }
}