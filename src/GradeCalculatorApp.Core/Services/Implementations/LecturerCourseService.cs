using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class LecturerCourseService : ILecturerCourseService
    {

        private readonly ILecturerCourseRepository _lecturerCourseRepository;
        private readonly ICourseRepository _courseRepository;
        
        public LecturerCourseService(ILecturerCourseRepository lecturerCourseRepository, ICourseRepository courseRepository)
        {
            _lecturerCourseRepository = lecturerCourseRepository;
            _courseRepository = courseRepository;
        }

        public bool CreateLecturerCourse(LecturerCourse lecturerCourse)
        {
            try
            {
                return lecturerCourse != null && _lecturerCourseRepository.CreateLecturerCourse(lecturerCourse);
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
                return _lecturerCourseRepository.ReadLecturerCourses(takeAll, count);
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
                return lecturerCourseId > 0 ? _lecturerCourseRepository.ReadLecturerCourse(lecturerCourseId) : null;
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
                return lecturerCourseId > 0 && _lecturerCourseRepository.DeleteLecturerCourse(lecturerCourseId);
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
                return lecturerCourseId > 0 && lecturerCourse != null && _lecturerCourseRepository.UpdateLecturerCourse(lecturerCourseId, lecturerCourse);
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
        public bool MapCourses(long lecturerCourseId, IEnumerable<long> courseIds)
        {
            try
            {
                var courses = new List<Course>();
                
                Parallel.ForEach(courseIds, courseId => courses.Add(_courseRepository.ReadCourse(courseId)));

                return _lecturerCourseRepository.MapCourses(lecturerCourseId, courses);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}