using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;

namespace GradeCalculatorApp.Core.Repositories.Interfaces
{
    public interface IRegisteredCourseRepository
    {
        bool CreateRegisteredCourses(List<RegisteredCourse> registeredCourses);
        IEnumerable<RegisteredCourseModel> ReadRegisteredCourses(long sessionSemesterId, long lecturerId);
        IEnumerable<RegisteredCourseModel> ReadRegisteredCoursesByStudent(long sessionSemesterId, long studentId);
        
        int CountTotalLecturerRegisteredCourses(long sessionSemesterId, long lecturerId);
        int CountTotalLecturerStudents(long sessionSemesterId, long lecturerId);
//        RegisteredCourse ReadRegisteredCourse(long courseId);
//        bool DeleteRegisteredCourse(long courseId);
//        bool UpdateRegisteredCourse(long courseId, RegisteredCourse course);
    }
}