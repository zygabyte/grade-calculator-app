using System;
using System.Collections.Generic;
using System.Linq;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Data;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Repositories.Implementations
{
    public class SessionCourseRepository : ISessionCourseRepository, IDisposable
    {
        private readonly GradeCalculatorContext _gradeCalculatorContext;
        
        public SessionCourseRepository(GradeCalculatorContext gradeCalculatorContext) => _gradeCalculatorContext = gradeCalculatorContext;
        
        public bool CreateSessionCourse(SessionCourse sessionCourse)
        {
            try
            {
                _gradeCalculatorContext.SessionCourses.Add(sessionCourse);

                return _gradeCalculatorContext.SaveChanges() > 0;
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
                return takeAll 
                    ? _gradeCalculatorContext.SessionCourses.Where(x => !x.IsDeleted && x.IsActive) 
                    : _gradeCalculatorContext.SessionCourses.Where(x => !x.IsDeleted && x.IsActive).Take(count);
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
                return _gradeCalculatorContext.SessionCourses.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == sessionCourseId);
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
                var sessionCourse = _gradeCalculatorContext.SessionCourses.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == sessionCourseId);

                if (sessionCourse == null) return false;
                
                sessionCourse.IsDeleted = true;
                sessionCourse.Modified = DateTime.Now;

                _gradeCalculatorContext.Entry(sessionCourse).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;

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
                var currentSessionCourse = _gradeCalculatorContext.SessionCourses.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == sessionCourseId);

                if (currentSessionCourse == null) return false;
                
                currentSessionCourse.Courses = sessionCourse.Courses;
                currentSessionCourse.SessionSemester = sessionCourse.SessionSemester;
                currentSessionCourse.SessionId = sessionCourse.SessionId;
                currentSessionCourse.Modified = DateTime.Now;
                    
                _gradeCalculatorContext.Entry(currentSessionCourse).State = EntityState.Modified;

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