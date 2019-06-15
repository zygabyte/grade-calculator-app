using System;
using System.Collections.Generic;
using System.Linq;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Data;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Repositories.Implementations
{
    public class SessionSemesterCourseRepository : ISessionSemesterCourseRepository, IDisposable
    {
        private readonly GradeCalculatorContext _gradeCalculatorContext;
        
        public SessionSemesterCourseRepository(GradeCalculatorContext gradeCalculatorContext) => _gradeCalculatorContext = gradeCalculatorContext;
        
        public bool CreateSessionCourse(SessionSemesterCourse sessionSemesterCourse)
        {
            try
            {
                _gradeCalculatorContext.SessionSemesterCourses.Add(sessionSemesterCourse);

                return _gradeCalculatorContext.SaveChanges() > 0;
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
                return takeAll 
                    ? _gradeCalculatorContext.SessionSemesterCourses.Where(x => !x.IsDeleted && x.IsActive)
                        .Include(x =>  x.SessionSemester)
                        .Include(x => x.Course)
                    : _gradeCalculatorContext.SessionSemesterCourses.Where(x => !x.IsDeleted && x.IsActive)
                        .Include(x =>  x.SessionSemester)
                        .Include(x => x.Course)
                        .Take(count);
            }
            catch (Exception e)
            {
                return new List<SessionSemesterCourse>();
            }
        }

        public IEnumerable<Course> ReadSessionCourse(long sessionSemesterId)
        {
            try
            {
                var sessionSemesterCourses = _gradeCalculatorContext.SessionSemesterCourses
                    .Include(x => x.Course)
                    .Where(x => !x.IsDeleted && x.SessionSemesterId == sessionSemesterId 
                                && !x.Course.IsDeleted)
                    .Select(x => x.Course)
                    .ToList();

                return sessionSemesterCourses;
            }
            catch (Exception e)
            {
                return new List<Course>();
            }
        }

        public bool DeleteSessionCourse(long sessionId, long courseId)
        {
            try
            {
                var sessionCourse = _gradeCalculatorContext.SessionSemesterCourses.FirstOrDefault(x => !x.IsDeleted && x.SessionSemesterId == sessionId && x.CourseId == courseId); 

                if (sessionCourse == null) return false;

                sessionCourse.IsDeleted = true;
                
                _gradeCalculatorContext.Entry(sessionCourse).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;
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
                var currentSessionCourse = _gradeCalculatorContext.SessionSemesterCourses.FirstOrDefault(x => !x.IsDeleted && x.Id == sessionCourseId);

                if (currentSessionCourse == null) return false;
                
                currentSessionCourse.SessionSemester = sessionSemesterCourse.SessionSemester;
                currentSessionCourse.SessionSemesterId = sessionSemesterCourse.SessionSemesterId;
                currentSessionCourse.Modified = DateTime.Now;
                    
                _gradeCalculatorContext.Entry(currentSessionCourse).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public bool MapCourses(long sessionSemesterId, List<long> courseIds)
        {
            try
            {
                courseIds.ForEach(courseId =>
                {
                    _gradeCalculatorContext.SessionSemesterCourses.Add(new SessionSemesterCourse
                    {
                        SessionSemesterId = sessionSemesterId,
                        CourseId = courseId
                    });
                });
                
                return _gradeCalculatorContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void Dispose()
        {
            _gradeCalculatorContext?.Dispose();
        }
    }
}