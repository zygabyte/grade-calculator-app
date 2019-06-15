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
                        .Include(x =>  x.Programme)
                        .Include(x => x.Course)
                    : _gradeCalculatorContext.ProgrammeCourses.Where(x => !x.IsDeleted && x.IsActive)
                        .Include(x =>  x.Programme)
                        .Include(x => x.Course)
                        .Take(count);
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
                var programmeCourse = _gradeCalculatorContext.ProgrammeCourses
                    .Include(x =>  x.Programme)
                    .Include(x => x.Course)
                    .FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == programmeCourseId);
                
                if (programmeCourse != null)
                {
//                    programmeCourse.Course = programmeCourse.Course.Where(x => !x.IsDeleted && x.IsActive).ToList();
                    return programmeCourse;
                }
                
//                return new ProgrammeCourse{ Course = new List<Course>() };
                return new ProgrammeCourse{ };
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool DeleteProgrammeCourse(long programmeCourseId, long courseId)
        {
            try
            {
                var programmeCourse = ReadProgrammeCourse(programmeCourseId);

                if (programmeCourse == null) return false;

//                programmeCourse.Course.Remove(
//                    programmeCourse.Course.FirstOrDefault(x => x.IsActive && !x.IsDeleted && x.Id == courseId));
                
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
                
                currentProgrammeCourse.Course = programmeCourse.Course;
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
        
        public bool MapCourses(long programmeId, List<long> courseIds)
        {
            try
            {
                courseIds.ForEach(courseId =>
                {
                    _gradeCalculatorContext.ProgrammeCourses.Add(new ProgrammeCourse
                    {
                        ProgrammeId = programmeId,
                        CourseId = courseId
                    });
                });
               
                return _gradeCalculatorContext.SaveChanges() > 0;
//
//                
//                if (currentProgrammeCourse == null || currentProgrammeCourse.Id == 0)
//                {
//                    _gradeCalculatorContext.ProgrammeCourses.Add(new ProgrammeCourse
//                    {
//                        ProgrammeId = programmeCourseId,
////                        Course = courses
//                    });
//                    
//                    return _gradeCalculatorContext.SaveChanges() > 0;
//                }
//
////                currentProgrammeCourse.Course.AddRange(courses);
//                    
//                _gradeCalculatorContext.Entry(currentProgrammeCourse).State = EntityState.Modified;
//
//                return _gradeCalculatorContext.SaveChanges() > 0;
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