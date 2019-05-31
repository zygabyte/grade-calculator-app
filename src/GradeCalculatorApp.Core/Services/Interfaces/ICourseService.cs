using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface ICourseService
    {
        bool CreateCourse(Course course);
        IEnumerable<Course> ReadCourses(bool takeAll = true, int count = 1000);
        Course ReadCourse(long courseId);
        bool DeleteCourse(long courseId);
        bool UpdateCourse(long courseId, Course course);
    }
}