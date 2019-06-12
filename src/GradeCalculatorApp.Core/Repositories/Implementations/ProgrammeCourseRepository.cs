using System;
using System.Collections.Generic;
using System.Linq;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Data;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Repositories.Implementations
{
    public class ProgrammeCourseRepository : IProgrammeCourseRepository, IDisposable
    {
        private readonly GradeCalculatorContext _gradeCalculatorContext;
        
        public ProgrammeCourseRepository(GradeCalculatorContext gradeCalculatorContext) => _gradeCalculatorContext = gradeCalculatorContext;
        
        public bool CreateProgrammeCourse(ProgrammeCourse programmeCourse)
        {
            try
            {
                _gradeCalculatorContext.ProgrammeCourses.Add(programmeCourse);

                return _gradeCalculatorContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<ProgrammeCourse> ReadProgrammeCourses(bool takeAll = true, int count = 1000)
        {
            try
            {
                return takeAll 
                    ? _gradeCalculatorContext.ProgrammeCourses.Where(x => !x.IsDeleted && x.IsActive) 
                    : _gradeCalculatorContext.ProgrammeCourses.Where(x => !x.IsDeleted && x.IsActive).Take(count);
            }
            catch (Exception e)
            {
                return new List<ProgrammeCourse>();
            }
        }

        public ProgrammeCourse ReadProgrammeCourse(long programmeCourseId)
        {
            try
            {
                return _gradeCalculatorContext.ProgrammeCourses.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == programmeCourseId);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool DeleteProgrammeCourse(long programmeCourseId)
        {
            try
            {
                var programmeCourse = _gradeCalculatorContext.ProgrammeCourses.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == programmeCourseId);

                if (programmeCourse == null) return false;
                
                programmeCourse.IsDeleted = true;
                programmeCourse.Modified = DateTime.Now;

                _gradeCalculatorContext.Entry(programmeCourse).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateProgrammeCourse(long programmeCourseId, ProgrammeCourse programmeCourse)
        {
            try
            {
                var currentProgrammeCourse = _gradeCalculatorContext.ProgrammeCourses.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == programmeCourseId);

                if (currentProgrammeCourse == null) return false;
                
                currentProgrammeCourse.Courses = programmeCourse.Courses;
                currentProgrammeCourse.Programme = programmeCourse.Programme;
                currentProgrammeCourse.ProgrammeId = programmeCourse.ProgrammeId;
                currentProgrammeCourse.Modified = DateTime.Now;
                    
                _gradeCalculatorContext.Entry(currentProgrammeCourse).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
        public bool MapCourses(long programmeCourseId, List<Course> courses)
        {
            try
            {
                var currentSessionCourse = _gradeCalculatorContext.ProgrammeCourses.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.ProgrammeId == programmeCourseId);

                if (currentSessionCourse == null)
                {
                    _gradeCalculatorContext.ProgrammeCourses.Add(new ProgrammeCourse
                    {
                        ProgrammeId = programmeCourseId,
                        Courses = courses
                    });
                    
                    return _gradeCalculatorContext.SaveChanges() > 0;
                }

                currentSessionCourse.Courses = courses;
                    
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