using System;
using System.Collections.Generic;
using System.Linq;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Data;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Repositories.Implementations
{
    public class DepartmentRepository : IDepartmentRepository, IDisposable
    {
        private readonly GradeCalculatorContext _gradeCalculatorContext;
        
        public DepartmentRepository(GradeCalculatorContext gradeCalculatorContext) => _gradeCalculatorContext = gradeCalculatorContext;
        
        public bool CreateDepartment(Department department)
        {
            try
            {
                _gradeCalculatorContext.Departments.Add(department);

                return _gradeCalculatorContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<Department> ReadDepartments(bool takeAll = true, int count = 1000)
        {
            try
            {
                return takeAll 
                    ? _gradeCalculatorContext.Departments
                        .Where(x => !x.IsDeleted && x.IsActive)
                        .Include(x => x.School)
                    : _gradeCalculatorContext.Departments
                        .Where(x => !x.IsDeleted && x.IsActive)
                        .Include(x => x.School)
                        .Take(count);
            }
            catch (Exception e)
            {
                return new List<Department>();
            }
        }

        public Department ReadDepartment(long departmentId)
        {
            try
            {
                return _gradeCalculatorContext.Departments
                    .Include(x => x.School)
                    .FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == departmentId);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool DeleteDepartment(long departmentId)
        {
            try
            {
                var department = _gradeCalculatorContext.Departments.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == departmentId);

                if (department == null) return false;
                
                department.IsDeleted = true;
                department.Modified = DateTime.Now;

                _gradeCalculatorContext.Entry(department).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateDepartment(long departmentId, Department department)
        {
            try
            {
                var currentDepartment = _gradeCalculatorContext.Departments.FirstOrDefault(x => !x.IsDeleted && x.IsActive && x.Id == departmentId);

                if (currentDepartment == null) return false;
                
                currentDepartment.Name = department.Name;
                currentDepartment.Code = department.Code;
                currentDepartment.School = department.School;
                currentDepartment.Lecturers = department.Lecturers;
                currentDepartment.Programmes = department.Programmes;
                currentDepartment.SchoolId = department.SchoolId;
                currentDepartment.Modified = DateTime.Now;
                    
                _gradeCalculatorContext.Entry(currentDepartment).State = EntityState.Modified;

                return _gradeCalculatorContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void Dispose()
        {
            _gradeCalculatorContext?.Dispose();
        }
    }
}