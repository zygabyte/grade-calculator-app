using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Repositories.Interfaces
{
    public interface ISessionRepository
    {
        bool CreateSession(Session session);
        IEnumerable<Session> ReadSessions(bool takeAll = true, int count = 1000);
        Session ReadSession(long sessionId);
        bool DeleteSession(long sessionId);
        bool UpdateSession(long sessionId, Session session);
    }
}