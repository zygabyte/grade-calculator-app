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
                        .Include(x => x.Courses)
                    : _gradeCalculatorContext.SessionSemesterCourses.Where(x => !x.IsDeleted && x.IsActive)
                        .Include(x =>  x.SessionSemester)
                        .Include(x => x.Courses)
                        .Take(count);
            }
            catch (Exception e)
            {
                return new List<SessionSemesterCourse>();
            }
        }

        public SessionSemesterCourse ReadSessionCourse(long sessionSemesterId)
        {
            try
            {
                return _gradeCalculatorContext.SessionSemesterCourses
                    .Include(x =>  x.SessionSemester)
                    .Include(x => x.Courses)
                    .FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.SessionSemesterId == sessionSemesterId);
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
                var sessionCourse = _gradeCalculatorContext.SessionSemesterCourses.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == sessionCourseId);

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

        public bool UpdateSessionCourse(long sessionCourseId, SessionSemesterCourse sessionSemesterCourse)
        {
            try
            {
                var currentSessionCourse = _gradeCalculatorContext.SessionSemesterCourses.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == sessionCourseId);

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


        public bool MapCourses(long sessionCourseId, List<Course> courses)
        {
            try
            {
                var currentSessionCourse = _gradeCalculatorContext.SessionSemesterCourses
                    .Include(x => x.Courses)
                    .FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.SessionSemesterId == sessionCourseId);

                if (currentSessionCourse == null)
                {
                    _gradeCalculatorContext.SessionSemesterCourses.Add(new SessionSemesterCourse
                    {
                        SessionSemesterId = sessionCourseId,
                        Courses = courses
                    });
                    
                    return _gradeCalculatorContext.SaveChanges() > 0;
                }

                currentSessionCourse.Courses.AddRange(courses);
                    
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