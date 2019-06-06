using System;
using System.Collections.Generic;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class SessionSemesterService : ISessionSemesterService
    {

        private readonly ISessionSemesterRepository _sessionSemesterRepository;
        
        public SessionSemesterService(ISessionSemesterRepository sessionSemesterRepository) => _sessionSemesterRepository = sessionSemesterRepository;
        
        public bool CreateSessionSemester(SessionSemester sessionSemester)
        {
            try
            {
                return sessionSemester != null && _sessionSemesterRepository.CreateSessionSemester(sessionSemester);
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
                return _sessionSemesterRepository.ReadSessionSemesters(takeAll, count);
            }
            catch (Exception e)
            {
                return new List<SessionSemester>();
            }
        }
        
        public bool CurrentExists()
        {
            try
            {
                return _sessionSemesterRepository.CurrentExists();
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public SessionSemester ReadSessionSemester(long sessionId)
        {
            try
            {
                return sessionId > 0 ? _sessionSemesterRepository.ReadSession(sessionId) : null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool DeleteSessionSemester(long sessionId)
        {
            try
            {
                return sessionId > 0 && _sessionSemesterRepository.DeleteSessionSemester(sessionId);
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
                return sessionId > 0 && sessionSemester != null && _sessionSemesterRepository.UpdateSessionSemester(sessionId, sessionSemester);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}