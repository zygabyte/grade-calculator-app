using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface IRegisteredCourseService
    {
        bool CreateRegisteredCourses(List<RegisteredCourse> registeredCourses);
        IEnumerable<RegisteredCourse> ReadRegisteredCourses(bool takeAll = true, int count = 1000);
    }
}