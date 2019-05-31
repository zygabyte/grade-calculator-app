using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface ISessionService
    {
        bool CreateSession(Session session);
        IEnumerable<Session> ReadSessions(bool takeAll = true, int count = 1000);
        Session ReadSession(long sessionId);
        bool DeleteSession(long sessionId);
        bool UpdateSession(long sessionId, Session session);
    }
}