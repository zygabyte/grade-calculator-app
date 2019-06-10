using System;
using System.Collections.Generic;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class SessionSemesterCourseService : ISessionSemesterCourseService
    {

        private readonly ISessionSemesterCourseRepository _sessionSemesterCourseRepository;
        
        public SessionSemesterCourseService(ISessionSemesterCourseRepository sessionSemesterCourseRepository) => _sessionSemesterCourseRepository = sessionSemesterCourseRepository;
        
        public bool CreateSessionCourse(SessionSemesterCourse sessionSemesterCourse)
        {
            try
            {
                return sessionSemesterCourse != null && _sessionSemesterCourseRepository.CreateSessionCourse(sessionSemesterCourse);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<SessionSemesterCourse> ReadSessionCourses(bool takeAll = true, int count = 1000)
        {
            try
            {
                return _sessionSemesterCourseRepository.ReadSessionCourses(takeAll, count);
            }
            catch (Exception e)
            {
                return new List<SessionSemesterCourse>();
            }
        }

        public SessionSemesterCourse ReadSessionCourse(long sessionCourseId)
        {
            try
            {
                return sessionCourseId > 0 ? _sessionSemesterCourseRepository.ReadSessionCourse(sessionCourseId) : null;
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
                return sessionCourseId > 0 && _sessionSemesterCourseRepository.DeleteSessionCourse(sessionCourseId);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateSessionCourse(long sessionCourseId, SessionSemesterCourse sessionSemesterCourse)
        {
            try
            {
                return sessionCourseId > 0 && sessionSemesterCourse != null && _sessionSemesterCourseRepository.UpdateSessionCourse(sessionCourseId, sessionSemesterCourse);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}