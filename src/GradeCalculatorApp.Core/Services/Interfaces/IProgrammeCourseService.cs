using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface IProgrammeCourseService
    {
        bool CreateProgrammeCourse(ProgrammeCourse programmeCourse);
        IEnumerable<ProgrammeCourse> ReadProgrammeCourses(bool takeAll = true, int count = 1000);
        IEnumerable<Course> ReadUniqueProgrammeCourses(long programmeId);
        ProgrammeCourse ReadProgrammeCourse(long programmeCourseId);
        bool DeleteProgrammeCourse(long programmeCourseId, long courseId);
        bool UpdateProgrammeCourse(long programmeCourseId, ProgrammeCourse programmeCourse);
        bool MapCourses(long programmeCourseId, List<long> courseIds);
    }
}