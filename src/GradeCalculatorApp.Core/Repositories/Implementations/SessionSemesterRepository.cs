using System;
using System.Collections.Generic;
using System.Linq;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Data;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Repositories.Implementations
{
    public class SessionSemesterRepository : ISessionSemesterRepository, IDisposable
    {
        private readonly GradeCalculatorContext _gradeCalculatorContext;
        
        public SessionSemesterRepository(GradeCalculatorContext gradeCalculatorContext) => _gradeCalculatorContext = gradeCalculatorContext;
        
        public bool CreateSessionSemester(SessionSemester sessionSemester)
        {
            try
            {
                _gradeCalculatorContext.SessionSemesters.Add(sessionSemester);

                return _gradeCalculatorContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<SessionSemester> ReadSessionSemesters(bool takeAll = true, int count = 1000)
        {
            try
            {
                return takeAll 
                    ? _gradeCalculatorContext.SessionSemesters.Where(x => !x.IsDeleted && x.IsActive)
                        .Include(x => x.Semester).Include(x => x.Session) 
                    : _gradeCalculatorContext.SessionSemesters.Where(x => !x.IsDeleted && x.IsActive)
                        .Include(x => x.Semester).Include(x => x.Semester).Include(x => x.Session)
                        .Take(count);
            }
            catch (Exception e)
            {
                return new List<SessionSemester>();
            }
        }

        public SessionSemester ReadSession(long sessionId)
        {
            try
            {
                return _gradeCalculatorContext.SessionSemesters
                    .Include(x => x.Semester).Include(x => x.Session) 
                    .FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == sessionId);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public SessionSemester ReadCurrentSessionSemester()
        {
            try
            {
                return _gradeCalculatorContext.SessionSemesters
                    .Include(x => x.Semester).Include(x => x.Session) 
                    .FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.IsCurrent);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool CurrentExists()
        {
            try
            {
                return _gradeCalculatorContext.SessionSemesters.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.IsCurrent) != null;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteSessionSemester(long sessionId)
        {
            try
            {
                var session = _gradeCalculatorContext.SessionSemesters.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == sessionId);

                if (session == null) return false;
                
                session.IsDeleted = true;
                session.Modified = DateTime.Now;

                _gradeCalculatorContext.Entry(session).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateSessionSemester(long sessionId, SessionSemester sessionSemester)
        {
            try
            {
                var currentSession = _gradeCalculatorContext.SessionSemesters.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == sessionId);

                if (currentSession == null) return false;
                
                currentSession.Semester = sessionSemester.Semester;
                currentSession.SemesterId = sessionSemester.SemesterId;
                currentSession.SemesterStartDate = sessionSemester.SemesterStartDate;
                currentSession.SemesterEndDate = sessionSemester.SemesterEndDate;
                currentSession.IsCurrent = sessionSemester.IsCurrent;
                currentSession.Modified = DateTime.Now;
                    
                _gradeCalculatorContext.Entry(currentSession).State = EntityState.Modified;

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