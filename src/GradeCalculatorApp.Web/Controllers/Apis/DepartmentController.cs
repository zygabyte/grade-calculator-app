using System;
using System.Linq;
using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;
using GradeCalculatorApp.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers.Apis
{
    public class DepartmentController : Controller
    {

        private readonly IDepartmentService _departmentService;
        private const string ObjectName = "Department"; 
        public DepartmentController(IDepartmentService departmentService) => _departmentService = departmentService;
        
        // GET
//        public IActionResult Index()
//        {
//            return
//            View();
//        }

        public ActionResult<ResponseData> CreateDepartment(Department department)
        {
            try
            {
                if (department == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));

                return _departmentService.CreateDepartment(department)  
                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulCreate, ObjectName)) 
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureCreate, ObjectName));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionCreate, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadDepartments()
        {
            try
            {
                return ResponseData.SendSuccessMsg(data: _departmentService.ReadDepartments().Select(x => new DepartmentVm
                {
                    Id = x.Id, Code = x.Code, Name = x.Name,
                    School = x.School.Name, SchoolId = x.SchoolId
                }));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionReadAll, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadDepartment(long departmentId)
        {
            try
            {
                var department = _departmentService.ReadDepartment(departmentId);
                
                return department != null
                    ? ResponseData.SendSuccessMsg(data: new DepartmentVm
                    {
                        Id = department.Id, Code = department.Code, Name = department.Name,
                        School = department.School.Name, SchoolId = department.SchoolId
                    })
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureRead, ObjectName, departmentId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionRead, ObjectName));
            }
        }

        public ActionResult<ResponseData> UpdateDepartment(long departmentId, Department department)
        {
            try
            {
                if (department == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));
                
                return _departmentService.UpdateDepartment(departmentId, department)  
                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulUpdate, ObjectName, departmentId)) 
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureUpdate, ObjectName, departmentId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionUpdate, ObjectName, departmentId));
            }
        }

        public ActionResult<ResponseData> DeleteDepartment(long departmentId)
        {
            try
            {
                return _departmentService.DeleteDepartment(departmentId)  
                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulDelete, ObjectName, departmentId)) 
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureDelete, ObjectName, departmentId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionDelete, ObjectName, departmentId));
            }
        }
    }
}