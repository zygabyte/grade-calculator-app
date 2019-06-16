using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface IGradeService
    {
        void CalculateFinalGrade(RegisteredCourseGrade registeredCourseGrade);
    }
}