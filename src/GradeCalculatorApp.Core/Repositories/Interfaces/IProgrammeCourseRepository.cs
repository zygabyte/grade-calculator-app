using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Repositories.Interfaces
{
    public interface IProgrammeCourseRepository
    {
        bool CreateProgrammeCourse(ProgrammeCourse programmeCourse);
        IEnumerable<ProgrammeCourse> ReadProgrammeCourses(bool takeAll = true, int count = 1000);
        IEnumerable<Course> ReadProgrammeCourse(long programmeId);
        bool DeleteProgrammeCourse(long programmeId, long courseId);
        bool UpdateProgrammeCourse(long programmeCourseId, ProgrammeCourse programmeCourse);
        bool MapCourses(long programmeId, List<long> courseIds);
    }
}