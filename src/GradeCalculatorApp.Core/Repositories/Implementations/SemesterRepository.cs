using System;
using System.Collections.Generic;
using System.Linq;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Data;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Repositories.Implementations
{
    public class SemesterRepository : ISemesterRepository
    {
        private readonly GradeCalculatorContext _gradeCalculatorContext;
        
        public SemesterRepository(GradeCalculatorContext gradeCalculatorContext) => _gradeCalculatorContext = gradeCalculatorContext;
        
        public bool CreateSemester(Semester semester)
        {
            try
            {
                _gradeCalculatorContext.Semesters.Add(semester);

                return _gradeCalculatorContext.SaveChanges() > 0;
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
                return takeAll 
                    ? _gradeCalculatorContext.Semesters.Where(x => !x.IsDeleted && x.IsActive) 
                    : _gradeCalculatorContext.Semesters.Where(x => !x.IsDeleted && x.IsActive).Take(count);
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
                return _gradeCalculatorContext.Semesters.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == semesterId);
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
                var semester = _gradeCalculatorContext.Semesters.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == semesterId);

                if (semester == null) return false;
                
                semester.IsDeleted = true;
                semester.Modified = DateTime.Now;

                _gradeCalculatorContext.Entry(semester).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;

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
                var currentSemester = _gradeCalculatorContext.Semesters.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == semesterId);

                if (currentSemester == null) return false;
                
                currentSemester.Name = semester.Name;
                currentSemester.Modified = DateTime.Now;
                    
                _gradeCalculatorContext.Entry(currentSemester).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}