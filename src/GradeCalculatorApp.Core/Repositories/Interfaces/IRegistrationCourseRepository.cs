using System.Collections.Generic;
using GradeCalculatorApp.Data.Models;

namespace GradeCalculatorApp.Core.Repositories.Interfaces
{
    public interface IRegistrationCourseRepository
    {
        IEnumerable<RegistrationCourse> ReadRegistrationCourses(long sessionSemesterId, long programmeId, long studentId);
    }
}