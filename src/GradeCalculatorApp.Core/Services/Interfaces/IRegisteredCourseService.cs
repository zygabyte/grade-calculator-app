using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface IRegisteredCourseService
    {
        bool CreateRegisteredCourses(List<RegisteredCourse> registeredCourses);
        IEnumerable<RegisteredCourseModel> ReadRegisteredCourses(long sessionSemesterId, long lecturerId);
        IEnumerable<RegisteredCourseModel> ReadRegisteredCoursesByStudent(long sessionSemesterId, long studentId);
    }
}