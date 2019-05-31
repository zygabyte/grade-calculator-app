using System;
using System.Collections.Generic;
using System.Linq;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Data;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Repositories.Implementations
{
    public class ProgrammeRepository : IProgrammeRepository
    {
        private readonly GradeCalculatorContext _gradeCalculatorContext;
        
        public ProgrammeRepository(GradeCalculatorContext gradeCalculatorContext) => _gradeCalculatorContext = gradeCalculatorContext;
        
        public bool CreateProgramme(Programme programme)
        {
            try
            {
                _gradeCalculatorContext.Programmes.Add(programme);

                return _gradeCalculatorContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<Programme> ReadProgrammes(bool takeAll = true, int count = 1000)
        {
            try
            {
                return takeAll 
                    ? _gradeCalculatorContext.Programmes.Where(x => !x.IsDeleted && x.IsActive) 
                    : _gradeCalculatorContext.Programmes.Where(x => !x.IsDeleted && x.IsActive).Take(count);
            }
            catch (Exception e)
            {
                return new List<Programme>();
            }
        }

        public Programme ReadProgramme(long programmeId)
        {
            try
            {
                return programmeId > 0 ? _gradeCalculatorContext.Programmes.FirstOrDefault(x => !x.IsDeleted && x.IsActive) : null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool DeleteProgramme(long programmeId)
        {
            try
            {
                var programme = _gradeCalculatorContext.Programmes.FirstOrDefault(x => !x.IsDeleted && x.IsActive);

                if (programme == null) return false;
                
                programme.IsDeleted = true;
                programme.Modified = DateTime.Now;

                _gradeCalculatorContext.Entry(programme).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateProgramme(long programmeId, Programme programme)
        {
            try
            {
                var currentProgramme = _gradeCalculatorContext.Programmes.FirstOrDefault(x => !x.IsDeleted && x.IsActive);

                if (programme == null || currentProgramme == null) return false;
                
                currentProgramme.Name = programme.Name;
                currentProgramme.Courses = programme.Courses;
                currentProgramme.Department = programme.Department;
                currentProgramme.DepartmentId = programme.DepartmentId;
                currentProgramme.Modified = DateTime.Now;
                    
                _gradeCalculatorContext.Entry(programme).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}