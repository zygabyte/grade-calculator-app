using System;
using System.Collections.Generic;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class SessionService : ISessionService
    {

        private readonly ISessionRepository _sessionRepository;
        
        public SessionService(ISessionRepository sessionRepository) => _sessionRepository = sessionRepository;
        
        public bool CreateSession(SessionSemester sessionSemester)
        {
            try
            {
                return sessionSemester != null && _sessionRepository.CreateSession(sessionSemester);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<SessionSemester> ReadSessions(bool takeAll = true, int count = 1000)
        {
            try
            {
                return _sessionRepository.ReadSessions(takeAll, count);
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
                return sessionId > 0 ? _sessionRepository.ReadSession(sessionId) : null;
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
                return sessionId > 0 && _sessionRepository.DeleteSession(sessionId);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateSession(long sessionId, SessionSemester sessionSemester)
        {
            try
            {
                return sessionId > 0 && sessionSemester != null && _sessionRepository.UpdateSession(sessionId, sessionSemester);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}