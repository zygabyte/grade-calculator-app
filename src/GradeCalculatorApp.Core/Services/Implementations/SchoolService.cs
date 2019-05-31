using System;
using System.Collections.Generic;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class SchoolService : ISchoolService
    {

        private readonly ISchoolRepository _schoolRepository;
        
        public SchoolService(ISchoolRepository schoolRepository) => _schoolRepository = schoolRepository;
        
        public bool CreateSchool(School school)
        {
            try
            {
                return school != null && _schoolRepository.CreateSchool(school);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<School> ReadSchools(bool takeAll = true, int count = 1000)
        {
            try
            {
                return _schoolRepository.ReadSchools(takeAll, count);
            }
            catch (Exception e)
            {
                return new List<School>();
            }
        }

        public School ReadSchool(long schoolId)
        {
            try
            {
                return schoolId > 0 ? _schoolRepository.ReadSchool(schoolId) : null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool DeleteSchool(long schoolId)
        {
            try
            {
                return schoolId > 0 && _schoolRepository.DeleteSchool(schoolId);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateSchool(long schoolId, School school)
        {
            try
            {
                return schoolId > 0 && school != null && _schoolRepository.UpdateSchool(schoolId, school);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}