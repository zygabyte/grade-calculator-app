using System;
using System.Collections.Generic;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class RegisteredCourseService : IRegisteredCourseService
    {
        private readonly IRegisteredCourseRepository _registeredCourseRepository;
        
        public RegisteredCourseService(IRegisteredCourseRepository registeredCourseRepository) => _registeredCourseRepository = registeredCourseRepository;

        public bool CreateRegisteredCourses(List<RegisteredCourse> registeredCourses)
        {
            try
            {
                return _registeredCourseRepository.CreateRegisteredCourses(registeredCourses);
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
                return _registeredCourseRepository.ReadRegisteredCourses(takeAll, count);
            }
            catch (Exception e)
            {
                return new List<RegisteredCourse>();
            }
        }
    }
}