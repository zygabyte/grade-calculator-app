using System;
using System.Collections.Generic;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class SessionCourseService : ISessionCourseService
    {

        private readonly ISessionCourseRepository _sessionCourseRepository;
        
        public SessionCourseService(ISessionCourseRepository sessionCourseRepository) => _sessionCourseRepository = sessionCourseRepository;
        
        public bool CreateSessionCourse(SessionCourse sessionCourse)
        {
            try
            {
                return sessionCourse != null && _sessionCourseRepository.CreateSessionCourse(sessionCourse);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<SessionCourse> ReadSessionCourses(bool takeAll = true, int count = 1000)
        {
            try
            {
                return _sessionCourseRepository.ReadSessionCourses(takeAll, count);
            }
            catch (Exception e)
            {
                return new List<SessionCourse>();
            }
        }

        public SessionCourse ReadSessionCourse(long sessionCourseId)
        {
            try
            {
                return sessionCourseId > 0 ? _sessionCourseRepository.ReadSessionCourse(sessionCourseId) : null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool DeleteSessionCourse(long sessionCourseId)
        {
            try
            {
                return sessionCourseId > 0 && _sessionCourseRepository.DeleteSessionCourse(sessionCourseId);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateSessionCourse(long sessionCourseId, SessionCourse sessionCourse)
        {
            try
            {
                return sessionCourseId > 0 && sessionCourse != null && _sessionCourseRepository.UpdateSessionCourse(sessionCourseId, sessionCourse);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}