using System.Collections.Generic;
using GradeCalculatorApp.Data.Models;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface IRegistrationCourseService
    {
        IEnumerable<RegistrationCourse> ReadRegistrationCourses(long sessionSemesterId, long programmeId);
    }
}