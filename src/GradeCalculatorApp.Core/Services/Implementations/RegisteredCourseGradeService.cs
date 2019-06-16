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
        private readonly IGradeService _gradeService;
        
        public RegisteredCourseGradeService(IRegisteredCourseGradeRepository registeredCourseGradeRepository, IGradeService gradeService)
        {
            _registeredCourseGradeRepository = registeredCourseGradeRepository;
            _gradeService = gradeService;
        }

        public bool CreateRegisteredCourseGrades(RegisteredCourseGrade registeredCourseGrade)
        {
            try
            {
                _gradeService.CalculateFinalGrade(registeredCourseGrade);
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