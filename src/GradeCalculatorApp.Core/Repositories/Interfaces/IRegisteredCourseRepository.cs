using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Repositories.Interfaces
{
    public interface IRegisteredCourseRepository
    {
        bool CreateRegisteredCourses(List<RegisteredCourse> registeredCourses);
        IEnumerable<RegisteredCourse> ReadRegisteredCourses(bool takeAll = true, int count = 1000);
//        RegisteredCourse ReadRegisteredCourse(long courseId);
//        bool DeleteRegisteredCourse(long courseId);
//        bool UpdateRegisteredCourse(long courseId, RegisteredCourse course);
    }
}