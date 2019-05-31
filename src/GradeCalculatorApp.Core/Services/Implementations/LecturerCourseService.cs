using System;
using System.Collections.Generic;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class LecturerCourseService : ILecturerCourseService
    {

        private readonly ILecturerCourseRepository _lecturerCourseRepository;
        
        public LecturerCourseService(ILecturerCourseRepository lecturerCourseRepository) => _lecturerCourseRepository = lecturerCourseRepository;
        
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
    }
}