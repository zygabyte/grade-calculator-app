using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Data;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Repositories.Implementations
{
    public class LecturerCourseRepository : ILecturerCourseRepository, IDisposable
    {
        private readonly GradeCalculatorContext _gradeCalculatorContext;
        
        public LecturerCourseRepository(GradeCalculatorContext gradeCalculatorContext) => _gradeCalculatorContext = gradeCalculatorContext;
        
        public bool CreateLecturerCourse(LecturerCourse lecturerCourse)
        {
            try
            {
                _gradeCalculatorContext.LecturerCourses.Add(lecturerCourse);

                return _gradeCalculatorContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<LecturerCourse> ReadLecturerCourses(bool takeAll = true, int count = 1000)
        {
            try
            {
                return takeAll 
                    ? _gradeCalculatorContext.LecturerCourses.Where(x => !x.IsDeleted && x.IsActive)
                        .Include(x =>  x.Lecturer)
                        .Include(x => x.Course)
                    : _gradeCalculatorContext.LecturerCourses.Where(x => !x.IsDeleted && x.IsActive)
                        .Include(x =>  x.Lecturer)
                        .Include(x => x.Course)
                        .Take(count);
            }
            catch (Exception e)
            {
                return new List<LecturerCourse>();
            }
        }

        public IEnumerable<Course> ReadLecturerCourse(long lecturerId)
        {
            try
            {
                var lecturerCourses = _gradeCalculatorContext.LecturerCourses
                    .Include(x => x.Course)
                    .Where(x => !x.IsDeleted && x.LecturerId == lecturerId 
                                             && !x.Course.IsDeleted)
                    .Select(x => x.Course)
                    .ToList();

                return lecturerCourses;
            }
            catch (Exception e)
            {
                return new List<Course>();
            }
        }

        public bool DeleteLecturerCourse(long lecturerId, long courseId)
        {
            try
            {
                var lecturerCourse = _gradeCalculatorContext.LecturerCourses.FirstOrDefault(x => !x.IsDeleted && x.LecturerId == lecturerId && x.CourseId == courseId); 

                if (lecturerCourse == null) return false;

                lecturerCourse.IsDeleted = true;
                
                _gradeCalculatorContext.Entry(lecturerCourse).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateLecturerCourse(long lecturerCourseId, LecturerCourse lecturerCourse)
        {
            try
            {
                var currentLecturerCourse = _gradeCalculatorContext.LecturerCourses.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == lecturerCourseId);

                if (currentLecturerCourse == null) return false;
                
                currentLecturerCourse.Course = lecturerCourse.Course;
                currentLecturerCourse.Lecturer = lecturerCourse.Lecturer;
                currentLecturerCourse.LecturerId = lecturerCourse.LecturerId;
                currentLecturerCourse.Modified = DateTime.Now;
                    
                _gradeCalculatorContext.Entry(currentLecturerCourse).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
        public bool MapCourses(long lecturerId, List<long> courseIds)
        {
            try
            {
                courseIds.ForEach(courseId =>
                {
                    _gradeCalculatorContext.LecturerCourses.Add(new LecturerCourse
                    {
                        LecturerId = lecturerId,
                        CourseId = courseId
                    });
                });
                    
                return _gradeCalculatorContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void Dispose()
        {
            _gradeCalculatorContext?.Dispose();
        }
    }
}