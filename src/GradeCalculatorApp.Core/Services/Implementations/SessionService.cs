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
        
        public bool CreateSession(Session session)
        {
            try
            {
                return session != null && _sessionRepository.CreateSession(session);
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
                return _sessionRepository.ReadSessions(takeAll, count);
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

        public bool UpdateSession(long sessionId, Session session)
        {
            try
            {
                return sessionId > 0 && session != null && _sessionRepository.UpdateSession(sessionId, session);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}