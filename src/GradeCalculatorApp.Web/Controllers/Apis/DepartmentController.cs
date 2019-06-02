using System;
using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;
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

                return ResponseData.SendSuccessMsg(_departmentService.CreateDepartment(department) 
                    ? string.Format(DefaultConstants.SuccessfulCreate, ObjectName) 
                    : string.Format(DefaultConstants.FailureCreate, ObjectName));
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
                return ResponseData.SendSuccessMsg(data: _departmentService.ReadDepartments());
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
                    ? ResponseData.SendSuccessMsg(data: department)
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
                
                return ResponseData.SendSuccessMsg(_departmentService.UpdateDepartment(departmentId, department) 
                    ? string.Format(DefaultConstants.SuccessfulUpdate, ObjectName, departmentId) 
                    : string.Format(DefaultConstants.FailureUpdate, ObjectName, departmentId));
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
                return ResponseData.SendSuccessMsg(_departmentService.DeleteDepartment(departmentId) 
                    ? string.Format(DefaultConstants.SuccessfulDelete, ObjectName, departmentId) 
                    : string.Format(DefaultConstants.FailureDelete, ObjectName, departmentId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionDelete, ObjectName, departmentId));
            }
        }
    }
}