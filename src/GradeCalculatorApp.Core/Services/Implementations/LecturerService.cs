using System;
using System.Collections.Generic;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class LecturerService : ILecturerService
    {

        private readonly ILecturerRepository _lecturerRepository;
        
        public LecturerService(ILecturerRepository lecturerRepository) => _lecturerRepository = lecturerRepository;
        
        public bool CreateLecturer(Lecturer lecturer)
        {
            try
            {
                return lecturer != null && _lecturerRepository.CreateLecturer(lecturer);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<Lecturer> ReadLecturers(bool takeAll = true, int count = 1000)
        {
            try
            {
                return _lecturerRepository.ReadLecturers(takeAll, count);
            }
            catch (Exception e)
            {
                return new List<Lecturer>();
            }
        }

        public Lecturer ReadLecturer(long lecturerId)
        {
            try
            {
                return lecturerId > 0 ? _lecturerRepository.ReadLecturer(lecturerId) : null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool DeleteLecturer(long lecturerId)
        {
            try
            {
                return lecturerId > 0 && _lecturerRepository.DeleteLecturer(lecturerId);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateLecturer(long lecturerId, Lecturer lecturer)
        {
            try
            {
                return lecturerId > 0 && lecturer != null && _lecturerRepository.UpdateLecturer(lecturerId, lecturer);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}