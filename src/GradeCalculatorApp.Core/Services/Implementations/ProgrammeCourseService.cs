using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class ProgrammeCourseService : IProgrammeCourseService
    {

        private readonly IProgrammeCourseRepository _programmeCourseRepository;
        private readonly ICourseRepository _courseRepository;
        
        public ProgrammeCourseService(IProgrammeCourseRepository programmeCourseRepository, ICourseRepository courseRepository)
        {
            _programmeCourseRepository = programmeCourseRepository;
            _courseRepository = courseRepository;
        }

        public bool CreateProgrammeCourse(ProgrammeCourse programmeCourse)
        {
            try
            {
                return programmeCourse != null && _programmeCourseRepository.CreateProgrammeCourse(programmeCourse);
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
                return _programmeCourseRepository.ReadProgrammeCourses(takeAll, count);
            }
            catch (Exception e)
            {
                return new List<ProgrammeCourse>();
            }
        }
        
        public IEnumerable<Course> ReadUniqueProgrammeCourses(long programmeId)
        {
            try
            {
                var programmeCourses = _programmeCourseRepository.ReadProgrammeCourse(programmeId).Courses;
                var allCourses = _courseRepository.ReadCourses();

                var uniqueCourses = new List<Course>();

                Parallel.ForEach(allCourses, course =>
                {
                    if (!programmeCourses.Contains(course)) uniqueCourses.Add(course); 
                });

                return uniqueCourses;
            }
            catch (Exception e)
            {
                return new List<Course>();
            }
        }

        public ProgrammeCourse ReadProgrammeCourse(long programmeCourseId)
        {
            try
            {
                return programmeCourseId > 0 ? _programmeCourseRepository.ReadProgrammeCourse(programmeCourseId) : null;
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
                return programmeCourseId > 0 && _programmeCourseRepository.DeleteProgrammeCourse(programmeCourseId, courseId);
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
                return programmeCourseId > 0 && programmeCourse != null && _programmeCourseRepository.UpdateProgrammeCourse(programmeCourseId, programmeCourse);
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
        public bool MapCourses(long programmeCourseId, IEnumerable<long> courseIds)
        {
            try
            {
                var courses = courseIds.Select(courseId => _courseRepository.ReadCourse(courseId)).ToList();

                return _programmeCourseRepository.MapCourses(programmeCourseId, courses);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}