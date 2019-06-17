using System.Collections.Generic;
using GradeCalculatorApp.Data.Models;

namespace GradeCalculatorApp.Core.Repositories.Interfaces
{
    public interface IGradeRepository
    {
        IEnumerable<GradedCourse> ReadGradedCourse(long sessionSemesterId, long studentId);
    }
}