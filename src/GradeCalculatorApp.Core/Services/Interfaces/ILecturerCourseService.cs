using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface ILecturerCourseService
    {
        bool CreateLecturerCourse(LecturerCourse lecturerCourse);
        IEnumerable<LecturerCourse> ReadLecturerCourses(bool takeAll = true, int count = 1000);
        LecturerCourse ReadLecturerCourse(long lecturerCourseId);
        bool DeleteLecturerCourse(long lecturerCourseId);
        bool UpdateLecturerCourse(long lecturerCourseId, LecturerCourse lecturerCourse);
        bool MapCourses(long lecturerCourseId, IEnumerable<long> courseIds);
    }
}