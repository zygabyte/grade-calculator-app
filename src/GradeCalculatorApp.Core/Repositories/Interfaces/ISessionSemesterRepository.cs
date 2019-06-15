using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Repositories.Interfaces
{
    public interface ISessionSemesterRepository
    {
        bool CreateSessionSemester(SessionSemester sessionSemester);
        IEnumerable<SessionSemester> ReadSessionSemesters(bool takeAll = true, int count = 1000);
        bool CurrentExists();
        SessionSemester ReadSession(long sessionId);
        SessionSemester ReadCurrentSessionSemester();
        bool DeleteSessionSemester(long sessionId);
        bool UpdateSessionSemester(long sessionId, SessionSemester sessionSemester);
    }
}