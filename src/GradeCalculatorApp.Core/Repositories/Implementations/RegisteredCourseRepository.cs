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

        public IEnumerable<RegisteredCourse> ReadRegisteredCourses(bool takeAll = true, int count = 1000)
        {
            try
            {
                return takeAll 
                    ? _gradeCalculatorContext.RegisteredCourses.Where(x => !x.IsDeleted && x.IsActive)
                        .Include(x => x.Course)
                        .Include(x => x.Student).Include(x => x.Lecturer)
                    : _gradeCalculatorContext.RegisteredCourses.Where(x => !x.IsDeleted && x.IsActive)
                        .Include(x => x.Course)
                        .Include(x => x.Student).Include(x => x.Lecturer)
                        .Take(count);
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