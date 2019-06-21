using System;
using System.Collections.Generic;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Models;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class RegistrationCourseService : IRegistrationCourseService
    {
        private readonly IRegistrationCourseRepository _registrationCourseRepository;
        
        public RegistrationCourseService(IRegistrationCourseRepository registrationCourseRepository) => _registrationCourseRepository = registrationCourseRepository;
        
        public IEnumerable<RegistrationCourse> ReadRegistrationCourses(long sessionSemesterId, long programmeId, long studentId)
        {
            try
            {
                return _registrationCourseRepository.ReadRegistrationCourses(sessionSemesterId, programmeId, studentId);
            }
            catch (Exception e)
            {
                return new List<RegistrationCourse>();
            }
        }
    }
}