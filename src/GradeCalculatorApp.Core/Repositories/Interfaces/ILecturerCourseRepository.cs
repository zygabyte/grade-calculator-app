using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Repositories.Interfaces
{
    public interface ILecturerCourseRepository
    {
        bool CreateLecturerCourse(LecturerCourse lecturerCourse);
        IEnumerable<LecturerCourse> ReadLecturerCourses(bool takeAll = true, int count = 1000);
        IEnumerable<Course> ReadLecturerCourse(long lecturerCourseId);
        bool DeleteLecturerCourse(long lecturerId, long courseId);
        bool UpdateLecturerCourse(long lecturerCourseId, LecturerCourse lecturerCourse);
        bool MapCourses(long lecturerId, List<long> courseIds);
        
        int CountTotalLecturerCourses(long lecturerId);
    }
}