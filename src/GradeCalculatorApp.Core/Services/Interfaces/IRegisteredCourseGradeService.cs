using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface IRegisteredCourseGradeService
    {
        bool CreateRegisteredCourseGrades(RegisteredCourseGrade registeredCourseGrade);
        IEnumerable<RegisteredCourseGrade> ReadRegisteredCourseGrades(bool takeAll = true, int count = 1000);
    }
}