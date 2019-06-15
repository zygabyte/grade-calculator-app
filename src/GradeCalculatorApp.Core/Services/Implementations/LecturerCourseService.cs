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
        
        public IEnumerable<Course> ReadUniqueLecturerCourses(long lecturerId)
        {
            try
            {
                var lecturerCourses = _lecturerCourseRepository.ReadLecturerCourse(lecturerId);
                var allCourses = _courseRepository.ReadCourses();

                var uniqueCourses = new List<Course>();

                Parallel.ForEach(allCourses, course =>
                {
                    if (!lecturerCourses.Contains(course)) uniqueCourses.Add(course); 
                });

                return uniqueCourses;
            }
            catch (Exception e)
            {
                return new List<Course>();
            }
        }

        public IEnumerable<Course> ReadLecturerCourse(long lecturerId)
        {
            try
            {
                return _lecturerCourseRepository.ReadLecturerCourse(lecturerId);
            }
            catch (Exception e)
            {
                return new List<Course>();
            }
        }

        public bool DeleteLecturerCourse(long lecturerCourseId, long courseId)
        {
            try
            {
                return lecturerCourseId > 0 && _lecturerCourseRepository.DeleteLecturerCourse(lecturerCourseId, courseId);
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
        
        public bool MapCourses(long lecturerId, List<long> courseIds)
        {
            try
            {
                return _lecturerCourseRepository.MapCourses(lecturerId, courseIds);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}