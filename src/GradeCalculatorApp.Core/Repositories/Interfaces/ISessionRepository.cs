using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Repositories.Interfaces
{
    public interface ISessionRepository
    {
        bool CreateSession(SessionSemester sessionSemester);
        IEnumerable<SessionSemester> ReadSessions(bool takeAll = true, int count = 1000);
        SessionSemester ReadSession(long sessionId);
        bool DeleteSession(long sessionId);
        bool UpdateSession(long sessionId, SessionSemester sessionSemester);
    }
}