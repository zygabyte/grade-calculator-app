using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface IGradeService
    {
        void CalculateFinalGrade(RegisteredCourseGrade registeredCourseGrade);
        IEnumerable<GradedCourse> ReadGradedCourses(long sessionSemesterId, long studentId);
    }
}