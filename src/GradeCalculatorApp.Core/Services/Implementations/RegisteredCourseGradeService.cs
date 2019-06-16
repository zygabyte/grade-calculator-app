using System;
using System.Collections.Generic;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class RegisteredCourseGradeService : IRegisteredCourseGradeService
    {
        private readonly IRegisteredCourseGradeRepository _registeredCourseGradeRepository;
        
        public RegisteredCourseGradeService(IRegisteredCourseGradeRepository registeredCourseGradeRepository) => _registeredCourseGradeRepository = registeredCourseGradeRepository;
        public bool CreateRegisteredCourseGrades(RegisteredCourseGrade registeredCourseGrade)
        {
            try
            {
                return _registeredCourseGradeRepository.CreateRegisteredCourseGrades(registeredCourseGrade);
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
                return _registeredCourseGradeRepository.ReadRegisteredCourseGrades(takeAll, count);
            }
            catch (Exception e)
            {
                return new List<RegisteredCourseGrade>();
            }
        }
    }
}