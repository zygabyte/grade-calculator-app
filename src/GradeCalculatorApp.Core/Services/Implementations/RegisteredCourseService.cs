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

        public IEnumerable<RegisteredCourse> ReadRegisteredCourses(long sessionSemesterId, long lecturerId)
        {
            try
            {
                return _registeredCourseRepository.ReadRegisteredCourses(sessionSemesterId, lecturerId);
            }
            catch (Exception e)
            {
                return new List<RegisteredCourse>();
            }
        }
    }
}