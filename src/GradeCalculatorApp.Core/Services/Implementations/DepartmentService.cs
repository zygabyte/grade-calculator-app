using System;
using System.Collections.Generic;
using GradeCalculatorApp.Core.Repositories.Interfaces;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {

        private readonly IDepartmentRepository _departmentRepository;
        
        public DepartmentService(IDepartmentRepository departmentRepository) => _departmentRepository = departmentRepository;
        
        public bool CreateDepartment(Department department)
        {
            try
            {
                return department != null && _departmentRepository.CreateDepartment(department);
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
                return _departmentRepository.ReadDepartments(takeAll, count);
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
                return departmentId > 0 ? _departmentRepository.ReadDepartment(departmentId) : null;
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
                return departmentId > 0 && _departmentRepository.DeleteDepartment(departmentId);
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
                return departmentId > 0 && department != null && _departmentRepository.UpdateDepartment(departmentId, department);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}