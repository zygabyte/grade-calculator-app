using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface ISessionSemesterCourseService
    {
        bool CreateSessionCourse(SessionSemesterCourse sessionSemesterCourse);
        IEnumerable<SessionSemesterCourse> ReadSessionCourses(bool takeAll = true, int count = 1000);
        IEnumerable<Course> ReadUniqueSessionCourses(long sessionSemesterId);
        SessionSemesterCourse ReadSessionCourse(long sessionSemesterId);
        bool DeleteSessionCourse(long sessionCourseId, long courseId);
        bool UpdateSessionCourse(long sessionCourseId, SessionSemesterCourse sessionSemesterCourse);
        bool MapCourses(long lecturerCourseId, IEnumerable<long> courseIds);
    }
}