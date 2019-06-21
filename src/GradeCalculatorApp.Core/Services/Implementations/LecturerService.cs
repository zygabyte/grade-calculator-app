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
        private readonly IMailService _mailService;
        
        public LecturerService(ILecturerRepository lecturerRepository, IMailService mailService)
        {
            _lecturerRepository = lecturerRepository;
            _mailService = mailService;
        }

        public bool CreateLecturer(Lecturer lecturer)
        {
            try
            {
                var tokenMap = Guid.NewGuid().ToString().Replace("-", "0");
                if (lecturer != null && _lecturerRepository.CreateLecturer(lecturer, tokenMap))
                {
                    _mailService.SendRegisterMail(lecturer.Email, $"{lecturer.FirstName} {lecturer.LastName}", lecturer.UserRole.ToString(), tokenMap);
                    return true;
                }

                return false;
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
        
        public Lecturer ReadLecturerByEmail(string email)
        {
            try
            {
                return _lecturerRepository.ReadLecturerByEmail(email);
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