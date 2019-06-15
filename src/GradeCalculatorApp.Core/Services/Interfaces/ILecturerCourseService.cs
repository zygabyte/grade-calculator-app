using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface ILecturerCourseService
    {
        bool CreateLecturerCourse(LecturerCourse lecturerCourse);
        IEnumerable<LecturerCourse> ReadLecturerCourses(bool takeAll = true, int count = 1000);
        IEnumerable<Course> ReadUniqueLecturerCourses(long lecturerId);
        LecturerCourse ReadLecturerCourse(long lecturerCourseId);
        bool DeleteLecturerCourse(long lecturerCourseId, long courseId);
        bool UpdateLecturerCourse(long lecturerCourseId, LecturerCourse lecturerCourse);
        bool MapCourses(long lecturerId, List<long> courseIds);
    }
}