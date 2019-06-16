using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface IRegisteredCourseService
    {
        bool CreateRegisteredCourses(List<RegisteredCourse> registeredCourses);
        IEnumerable<RegisteredCourse> ReadRegisteredCourses(long sessionSemesterId, long lecturerId);
    }
}