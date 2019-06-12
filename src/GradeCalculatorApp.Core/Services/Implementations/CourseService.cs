using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class CourseService : ICourseService
    {

        private readonly ICourseRepository _courseRepository;
        
        public CourseService(ICourseRepository courseRepository) => _courseRepository = courseRepository;
        
        public bool CreateCourse(Course course)
        {
            try
            {
                return course != null && _courseRepository.CreateCourse(course);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<Course> ReadCourses(bool takeAll = true, int count = 1000)
        {
            try
            {
                return _courseRepository.ReadCourses(takeAll, count);
            }
            catch (Exception e)
            {
                return new List<Course>();
            }
        }

        public Course ReadCourse(long courseId)
        {
            try
            {
                return courseId > 0 ? _courseRepository.ReadCourse(courseId) : null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool DeleteCourse(long courseId)
        {
            try
            {
                return courseId > 0 && _courseRepository.DeleteCourse(courseId);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateCourse(long courseId, Course course)
        {
            try
            {
                return courseId > 0 && course != null && _courseRepository.UpdateCourse(courseId, course);
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
//        public bool MapCourseToSessionSemesterCourse(long sessionSemesterCourseId, List<long> courseIds)
//        {
//            try
//            {
//                Parallel.ForEach(courseIds, courseId => _courseRepository.MapCourseToSessionSemesterCourse(sessionSemesterCourseId, courseId));
//
//                return true;
//            }
//            catch (Exception e)
//            {
//                return false;
//            }
//        }
//
//        public bool MapCourseToProgrammeCourse(long programmeCourseId, List<long> courseIds)
//        {
//            try
//            {
//                Parallel.ForEach(courseIds, courseId => _courseRepository.MapCourseToProgrammeCourse(programmeCourseId, courseId));
//
//                return true;
//            }
//            catch (Exception e)
//            {
//                return false;
//            }
//        }
//
//        public bool MapCourseToLecturerCourse(long lecturerCourseId, List<long> courseIds)
//        {
//            try
//            {
//                Parallel.ForEach(courseIds, courseId => _courseRepository.MapCourseToLecturerCourse(lecturerCourseId, courseId));
//
//                return true;
//            }
//            catch (Exception e)
//            {
//                return false;
//            }
//        }
    }
}