using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Repositories.Interfaces
{
    public interface ILecturerCourseRepository
    {
        bool CreateLecturerCourse(LecturerCourse lecturerCourse);
        IEnumerable<LecturerCourse> ReadLecturerCourses(bool takeAll = true, int count = 1000);
        LecturerCourse ReadLecturerCourse(long lecturerCourseId);
        bool DeleteLecturerCourse(long lecturerCourseId);
        bool UpdateLecturerCourse(long lecturerCourseId, LecturerCourse lecturerCourse);
    }
}