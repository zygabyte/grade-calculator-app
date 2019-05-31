using System;
using System.Collections.Generic;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class SemesterService : ISemesterService
    {

        private readonly ISemesterRepository _semesterRepository;
        
        public SemesterService(ISemesterRepository semesterRepository) => _semesterRepository = semesterRepository;
        
        public bool CreateSemester(Semester semester)
        {
            try
            {
                return semester != null && _semesterRepository.CreateSemester(semester);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<Semester> ReadSemesters(bool takeAll = true, int count = 1000)
        {
            try
            {
                return _semesterRepository.ReadSemesters(takeAll, count);
            }
            catch (Exception e)
            {
                return new List<Semester>();
            }
        }

        public Semester ReadSemester(long semesterId)
        {
            try
            {
                return semesterId > 0 ? _semesterRepository.ReadSemester(semesterId) : null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool DeleteSemester(long semesterId)
        {
            try
            {
                return semesterId > 0 && _semesterRepository.DeleteSemester(semesterId);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateSemester(long semesterId, Semester semester)
        {
            try
            {
                return semesterId > 0 && semester != null && _semesterRepository.UpdateSemester(semesterId, semester);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}