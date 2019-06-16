using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface IGradeService
    {
        RegisteredCourseGrade CalculateFinalGrade(RegisteredCourseGrade registeredCourseGrade);
    }
}