using System;
using System.Collections.Generic;
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

        public bool DeleteProgrammeCourse(long programmeCourseId)
        {
            try
            {
                return programmeCourseId > 0 && _programmeCourseRepository.DeleteProgrammeCourse(programmeCourseId);
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
                var courses = new List<Course>();
                
                Parallel.ForEach(courseIds, courseId => courses.Add(_courseRepository.ReadCourse(courseId)));

                return _programmeCourseRepository.MapCourses(programmeCourseId, courses);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}