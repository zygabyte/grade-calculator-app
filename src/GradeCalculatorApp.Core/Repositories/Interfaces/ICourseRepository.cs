using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        bool CreateCourse(Course course);
        IEnumerable<Course> ReadCourses(bool takeAll = true, int count = 1000);
        Course ReadCourse(long courseId);
        bool DeleteCourse(long courseId);
        bool UpdateCourse(long courseId, Course course);
//        bool MapCourseToSessionSemesterCourse(long sessionSemesterCourseId, long courseId);
//        bool MapCourseToProgrammeCourse(long programmeCourseId, long courseId);
//        bool MapCourseToLecturerCourse(long lecturerCourseId, long courseId);
    }
}