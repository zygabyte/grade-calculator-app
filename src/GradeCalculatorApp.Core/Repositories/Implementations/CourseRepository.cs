using System;
using System.Collections.Generic;
using System.Linq;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Data;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Repositories.Implementations
{
    public class CourseRepository : ICourseRepository, IDisposable
    {
        private readonly GradeCalculatorContext _gradeCalculatorContext;
        
        public CourseRepository(GradeCalculatorContext gradeCalculatorContext) => _gradeCalculatorContext = gradeCalculatorContext;
        
        public bool CreateCourse(Course course)
        {
            try
            {
                _gradeCalculatorContext.Courses.Add(course);

                return _gradeCalculatorContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool CreateCourses(List<Course> courses)
        {
            try
            {
                _gradeCalculatorContext.Courses.AddRange(courses);

                return  _gradeCalculatorContext.SaveChanges() > 0;
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
                return takeAll 
                    ? _gradeCalculatorContext.Courses.Where(x => !x.IsDeleted && x.IsActive) 
                    : _gradeCalculatorContext.Courses.Where(x => !x.IsDeleted && x.IsActive).Take(count);
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
                return _gradeCalculatorContext.Courses.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == courseId);
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
                var course = _gradeCalculatorContext.Courses.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == courseId);

                if (course == null) return false;
                
                course.IsDeleted = true;
                course.Modified = DateTime.Now;

                _gradeCalculatorContext.Entry(course).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;

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
                var currentCourse = _gradeCalculatorContext.Courses.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == courseId);

                if (currentCourse == null) return false;
                
                currentCourse.Code = course.Code;
                currentCourse.Name = course.Name;
                currentCourse.CreditUnit = course.CreditUnit;
                currentCourse.Modified = DateTime.Now;
                    
                _gradeCalculatorContext.Entry(currentCourse).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

//        public bool MapCourseToSessionSemesterCourse(long sessionSemesterCourseId, long courseId)
//        {
//            try
//            {
//                var currentCourse = _gradeCalculatorContext.Courses.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == courseId);
//
//                if (currentCourse == null) return false;
//
//                currentCourse.SessionSemesterCourseId = sessionSemesterCourseId;
//                    
//                _gradeCalculatorContext.Entry(currentCourse).State = EntityState.Modified;
//
//                return _gradeCalculatorContext.SaveChanges() > 0;
//            }
//            catch (Exception e)
//            {
//                return false;
//            }
//        }
//
//        public bool MapCourseToProgrammeCourse(long programmeCourseId, long courseId)
//        {
//            try
//            {
//                var currentCourse = _gradeCalculatorContext.Courses.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == courseId);
//
//                if (currentCourse == null) return false;
//
//                currentCourse.ProgrammeCourseId = programmeCourseId;
//                    
//                _gradeCalculatorContext.Entry(currentCourse).State = EntityState.Modified;
//
//                return _gradeCalculatorContext.SaveChanges() > 0;
//            }
//            catch (Exception e)
//            {
//                return false;
//            }
//        }
//
//        public bool MapCourseToLecturerCourse(long lecturerCourseId, long courseId)
//        {
//            try
//            {
//                var currentCourse = _gradeCalculatorContext.Courses.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == courseId);
//
//                if (currentCourse == null) return false;
//
//                currentCourse.LecturerCourseId = lecturerCourseId;
//                    
//                _gradeCalculatorContext.Entry(currentCourse).State = EntityState.Modified;
//
//                return _gradeCalculatorContext.SaveChanges() > 0;
//            }
//            catch (Exception e)
//            {
//                return false;
//            }
//        }

        public void Dispose()
        {
            _gradeCalculatorContext?.Dispose();
        }
    }
}