using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Repositories.Interfaces
{
    public interface ISessionSemesterCourseRepository
    {
        bool CreateSessionCourse(SessionSemesterCourse sessionSemesterCourse);
        IEnumerable<SessionSemesterCourse> ReadSessionCourses(bool takeAll = true, int count = 1000);
        IEnumerable<Course> ReadSessionCourse(long sessionSemesterId);
        bool DeleteSessionCourse(long sessionCourseId, long courseId);
        bool UpdateSessionCourse(long sessionCourseId, SessionSemesterCourse sessionSemesterCourse);
        bool MapCourses(long sessionSemesterId, List<long> courseIds);
    }
}