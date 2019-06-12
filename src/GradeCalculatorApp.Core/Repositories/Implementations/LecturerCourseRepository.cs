using System;
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
                        .Include(x => x.Courses)
                    : _gradeCalculatorContext.LecturerCourses.Where(x => !x.IsDeleted && x.IsActive)
                        .Include(x =>  x.Lecturer)
                        .Include(x => x.Courses)
                        .Take(count);
            }
            catch (Exception e)
            {
                return new List<LecturerCourse>();
            }
        }

        public LecturerCourse ReadLecturerCourse(long lecturerCourseId)
        {
            try
            {
                return _gradeCalculatorContext.LecturerCourses
                    .Include(x =>  x.Lecturer)
                    .Include(x => x.Courses)
                    .FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == lecturerCourseId);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool DeleteLecturerCourse(long lecturerCourseId)
        {
            try
            {
                var lecturerCourse = _gradeCalculatorContext.LecturerCourses.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == lecturerCourseId);

                if (lecturerCourse == null) return false;
                
                lecturerCourse.IsDeleted = true;
                lecturerCourse.Modified = DateTime.Now;

                _gradeCalculatorContext.Entry(lecturerCourse).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;
            }
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
                
                currentLecturerCourse.Courses = lecturerCourse.Courses;
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
        
        public bool MapCourses(long lecturerCourseId, List<Course> courses)
        {
            try
            {
                var currentSessionCourse = _gradeCalculatorContext.LecturerCourses.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.LecturerId == lecturerCourseId);

                if (currentSessionCourse == null)
                {
                    _gradeCalculatorContext.LecturerCourses.Add(new LecturerCourse
                    {
                        LecturerId = lecturerCourseId,
                        Courses = courses
                    });
                    
                    return _gradeCalculatorContext.SaveChanges() > 0;
                }

                currentSessionCourse.Courses.AddRange(courses);
                    
                _gradeCalculatorContext.Entry(currentSessionCourse).State = EntityState.Modified;

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