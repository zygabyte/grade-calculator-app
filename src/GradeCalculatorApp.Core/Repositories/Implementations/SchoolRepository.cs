using System;
using System.Collections.Generic;
using System.Linq;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Data;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Repositories.Implementations
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly GradeCalculatorContext _gradeCalculatorContext;
        
        public SchoolRepository(GradeCalculatorContext gradeCalculatorContext) => _gradeCalculatorContext = gradeCalculatorContext;
        
        public bool CreateSchool(School school)
        {
            try
            {
                _gradeCalculatorContext.Schools.Add(school);

                return _gradeCalculatorContext.SaveChanges() > 0;
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
                return takeAll 
                    ? _gradeCalculatorContext.Schools.Where(x => !x.IsDeleted && x.IsActive) 
                    : _gradeCalculatorContext.Schools.Where(x => !x.IsDeleted && x.IsActive).Take(count);
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
                return schoolId > 0 ? _gradeCalculatorContext.Schools.FirstOrDefault(x => !x.IsDeleted && x.IsActive) : null;
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
                var school = _gradeCalculatorContext.Schools.FirstOrDefault(x => !x.IsDeleted && x.IsActive);

                if (school == null) return false;
                
                school.IsDeleted = true;
                school.Modified = DateTime.Now;

                _gradeCalculatorContext.Entry(school).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;

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
                var currentSchool = _gradeCalculatorContext.Schools.FirstOrDefault(x => !x.IsDeleted && x.IsActive);

                if (school == null || currentSchool == null) return false;
                
                currentSchool.Name = school.Name;
                currentSchool.Departments = school.Departments;
                currentSchool.Modified = DateTime.Now;
                    
                _gradeCalculatorContext.Entry(school).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}