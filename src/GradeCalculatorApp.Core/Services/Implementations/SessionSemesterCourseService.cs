using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class SessionSemesterCourseService : ISessionSemesterCourseService
    {

        private readonly ISessionSemesterCourseRepository _sessionSemesterCourseRepository;
        private readonly ICourseRepository _courseRepository;
        
        public SessionSemesterCourseService(ISessionSemesterCourseRepository sessionSemesterCourseRepository, ICourseRepository courseRepository)
        {
            _sessionSemesterCourseRepository = sessionSemesterCourseRepository;
            _courseRepository = courseRepository;
        }

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
        
        public bool MapCourses(long lecturerCourseId, IEnumerable<long> courseIds)
        {
            try
            {
                var courses = new List<Course>();
                
                Parallel.ForEach(courseIds, courseId => courses.Add(_courseRepository.ReadCourse(courseId)));

                return _sessionSemesterCourseRepository.MapCourses(lecturerCourseId, courses);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}