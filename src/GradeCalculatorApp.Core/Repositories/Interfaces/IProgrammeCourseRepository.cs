using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Repositories.Interfaces
{
    public interface IProgrammeCourseRepository
    {
        bool CreateProgrammeCourse(ProgrammeCourse programmeCourse);
        IEnumerable<ProgrammeCourse> ReadProgrammeCourses(bool takeAll = true, int count = 1000);
        ProgrammeCourse ReadProgrammeCourse(long programmeCourseId);
        bool DeleteProgrammeCourse(long programmeCourseId);
        bool UpdateProgrammeCourse(long programmeCourseId, ProgrammeCourse programmeCourse);
    }
}