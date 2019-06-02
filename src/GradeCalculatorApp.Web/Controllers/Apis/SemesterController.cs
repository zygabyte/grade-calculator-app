using System;
using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers.Apis
{
    public class SemesterController : Controller
    {

        private readonly ISemesterService _semesterService;
        private const string ObjectName = "Semester"; 
        public SemesterController(ISemesterService semesterService) => _semesterService = semesterService;
        
        // GET
//        public IActionResult Index()
//        {
//            return
//            View();
//        }

        public ActionResult<ResponseData> CreateSemester(Semester semester)
        {
            try
            {
                if (semester == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));

                return ResponseData.SendSuccessMsg(_semesterService.CreateSemester(semester) 
                    ? string.Format(DefaultConstants.SuccessfulCreate, ObjectName) 
                    : string.Format(DefaultConstants.FailureCreate, ObjectName));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionCreate, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadSemesters()
        {
            try
            {
                return ResponseData.SendSuccessMsg(data: _semesterService.ReadSemesters());
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionReadAll, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadSemester(long semesterId)
        {
            try
            {
                var semester = _semesterService.ReadSemester(semesterId);

                return semester != null
                    ? ResponseData.SendSuccessMsg(data: semester)
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureRead, ObjectName, semesterId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionRead, ObjectName));
            }
        }

        public ActionResult<ResponseData> UpdateSemester(long semesterId, Semester semester)
        {
            try
            {
                if (semester == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));
                
                return ResponseData.SendSuccessMsg(_semesterService.UpdateSemester(semesterId, semester) 
                    ? string.Format(DefaultConstants.SuccessfulUpdate, ObjectName, semesterId) 
                    : string.Format(DefaultConstants.FailureUpdate, ObjectName, semesterId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionUpdate, ObjectName, semesterId));
            }
        }

        public ActionResult<ResponseData> DeleteSemester(long semesterId)
        {
            try
            {
                return ResponseData.SendSuccessMsg(_semesterService.DeleteSemester(semesterId) 
                    ? string.Format(DefaultConstants.SuccessfulDelete, ObjectName, semesterId) 
                    : string.Format(DefaultConstants.FailureDelete, ObjectName, semesterId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionDelete, ObjectName, semesterId));
            }
        }
    }
}