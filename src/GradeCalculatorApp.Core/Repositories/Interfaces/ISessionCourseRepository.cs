using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Repositories.Interfaces
{
    public interface ISessionCourseRepository
    {
        bool CreateSessionCourse(SessionCourse sessionCourse);
        IEnumerable<SessionCourse> ReadSessionCourses(bool takeAll = true, int count = 1000);
        SessionCourse ReadSessionCourse(long sessionCourseId);
        bool DeleteSessionCourse(long sessionCourseId);
        bool UpdateSessionCourse(long sessionCourseId, SessionCourse sessionCourse);
    }
}