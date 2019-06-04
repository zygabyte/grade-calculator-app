using System;
using System.Collections.Generic;
using System.Linq;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Data;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Repositories.Implementations
{
    public class SessionRepository : ISessionRepository
    {
        private readonly GradeCalculatorContext _gradeCalculatorContext;
        
        public SessionRepository(GradeCalculatorContext gradeCalculatorContext) => _gradeCalculatorContext = gradeCalculatorContext;
        
        public bool CreateSession(Session session)
        {
            try
            {
                _gradeCalculatorContext.Sessions.Add(session);

                return _gradeCalculatorContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<Session> ReadSessions(bool takeAll = true, int count = 1000)
        {
            try
            {
                return takeAll 
                    ? _gradeCalculatorContext.Sessions.Where(x => !x.IsDeleted && x.IsActive).Include(x => x.Semester) 
                    : _gradeCalculatorContext.Sessions.Where(x => !x.IsDeleted && x.IsActive).Include(x => x.Semester).Take(count);
            }
            catch (Exception e)
            {
                return new List<Session>();
            }
        }

        public Session ReadSession(long sessionId)
        {
            try
            {
                return _gradeCalculatorContext.Sessions.Include(x => x.Semester).FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == sessionId);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool DeleteSession(long sessionId)
        {
            try
            {
                var session = _gradeCalculatorContext.Sessions.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == sessionId);

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

        public bool UpdateSession(long sessionId, Session session)
        {
            try
            {
                var currentSession = _gradeCalculatorContext.Sessions.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == sessionId);

                if (currentSession == null) return false;
                
                currentSession.Courses = session.Courses;
                currentSession.Semester = session.Semester;
                currentSession.Name = session.Name;
                currentSession.SemesterId = session.SemesterId;
                currentSession.SemesterStartDate = session.SemesterStartDate;
                currentSession.SemesterEndDate = session.SemesterEndDate;
                currentSession.Modified = DateTime.Now;
                    
                _gradeCalculatorContext.Entry(currentSession).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}