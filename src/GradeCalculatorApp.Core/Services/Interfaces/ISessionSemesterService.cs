using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface ISessionSemesterService
    {
        bool CreateSessionSemester(SessionSemester sessionSemester);
        IEnumerable<SessionSemester> ReadSessionSemesters(bool takeAll = true, int count = 1000);
        SessionSemester ReadSessionSemester(long sessionId);
        bool DeleteSessionSemester(long sessionId);
        bool UpdateSessionSemester(long sessionId, SessionSemester sessionSemester);
    }
}