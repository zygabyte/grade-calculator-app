using System.Collections.Generic;
using GradeCalculatorApp.Data.Domains;

namespace GradeCalculatorApp.Core.Services.Interfaces
{
    public interface IDepartmentService
    {
        bool CreateDepartment(Department department);
        IEnumerable<Department> ReadDepartments(bool takeAll = true, int count = 1000);
        Department ReadDepartment(long departmentId);
        bool DeleteDepartment(long departmentId);
        bool UpdateDepartment(long departmentId, Department department);
    }
}