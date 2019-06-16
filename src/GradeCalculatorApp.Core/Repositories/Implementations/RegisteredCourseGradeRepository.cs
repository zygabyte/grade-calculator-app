using System;
using System.Collections.Generic;
using System.Linq;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Data;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Repositories.Implementations
{
    public class RegisteredCourseGradeRepository : IRegisteredCourseGradeRepository, IDisposable
    {
        private readonly GradeCalculatorContext _gradeCalculatorContext;
        
        public RegisteredCourseGradeRepository(GradeCalculatorContext gradeCalculatorContext) => _gradeCalculatorContext = gradeCalculatorContext;
        
        public bool CreateRegisteredCourseGrades(RegisteredCourseGrade registeredCourseGrade)
        {
            try
            {
                _gradeCalculatorContext.RegisteredCourseGrades.Add(registeredCourseGrade);

                return _gradeCalculatorContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<RegisteredCourseGrade> ReadRegisteredCourseGrades(bool takeAll = true, int count = 1000)
        {
            try
            {
                return _gradeCalculatorContext.RegisteredCourseGrades
                    .Include(x => x.RegisteredCourse)
                    .Where(x => !x.IsDeleted && !x.RegisteredCourse.IsDeleted);
            }
            catch (Exception e)
            {
                return new List<RegisteredCourseGrade>();
            }
        }

        public void Dispose()
        {
            _gradeCalculatorContext?.Dispose();
        }
    }
}