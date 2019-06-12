using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradeCalculatorApp.Core.Repositories.Implementations;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data;
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

        public IEnumerable<Course> ReadUniqueSessionCourses(long sessionSemesterId)
        {
            try
            {
                var sessionCourses = _sessionSemesterCourseRepository.ReadSessionCourse(sessionSemesterId).Courses;
                var allCourses = _courseRepository.ReadCourses();

                var uniqueCourses = new List<Course>();

                Parallel.ForEach(allCourses, course =>
                {
                    if (!sessionCourses.Contains(course)) uniqueCourses.Add(course); 
                });

                return uniqueCourses;
            }
            catch (Exception e)
            {
                return new List<Course>();
            }
        }

        public SessionSemesterCourse ReadSessionCourse(long sessionSemesterId)
        {
            try
            {
                return sessionSemesterId > 0 ? _sessionSemesterCourseRepository.ReadSessionCourse(sessionSemesterId) : null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool DeleteSessionCourse(long sessionCourseId, long courseId)
        {
            try
            {
                return sessionCourseId > 0 && _sessionSemesterCourseRepository.DeleteSessionCourse(sessionCourseId, courseId);
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
        
        public bool MapCourses(long sessionSemesterCourseId, IEnumerable<long> courseIds)
        {
            try
            {
                var courses = courseIds.Select(courseId => _courseRepository.ReadCourse(courseId)).ToList();

                return _sessionSemesterCourseRepository.MapCourses(sessionSemesterCourseId, courses);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}