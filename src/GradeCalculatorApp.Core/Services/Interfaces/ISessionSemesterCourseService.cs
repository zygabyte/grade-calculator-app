using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface ISessionSemesterCourseService
    {
        bool CreateSessionCourse(SessionSemesterCourse sessionSemesterCourse);
        IEnumerable<SessionSemesterCourse> ReadSessionCourses(bool takeAll = true, int count = 1000);
        SessionSemesterCourse ReadSessionCourse(long sessionCourseId);
        bool DeleteSessionCourse(long sessionCourseId);
        bool UpdateSessionCourse(long sessionCourseId, SessionSemesterCourse sessionSemesterCourse);
    }
}