using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;

namespace GradeCalculatorApp.Core.Repositories.Interfaces
{
    public interface IGradeRepository
    {
        IEnumerable<GradedCourse> ReadGradedCourses(long sessionSemesterId, long studentId);
        RegisteredCourseGrade ReadGradedCourse(long gradedCourseId);
    }
}