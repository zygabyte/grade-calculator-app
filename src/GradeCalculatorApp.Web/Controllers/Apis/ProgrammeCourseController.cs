using System;
using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers.Apis
{
    public class ProgrammeCourseController : Controller
    {

        private readonly IProgrammeCourseService _programmeCourseService;
        private const string ObjectName = "ProgrammeCourse"; 
        public ProgrammeCourseController(IProgrammeCourseService programmeCourseService) => _programmeCourseService = programmeCourseService;
        
        // GET
//        public IActionResult Index()
//        {
//            return
//            View();
//        }

        public ActionResult<ResponseData> CreateProgrammeCourse(ProgrammeCourse programmeCourse)
        {
            try
            {
                if (programmeCourse == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));

                return ResponseData.SendSuccessMsg(_programmeCourseService.CreateProgrammeCourse(programmeCourse) 
                    ? string.Format(DefaultConstants.SuccessfulCreate, ObjectName) 
                    : string.Format(DefaultConstants.FailureCreate, ObjectName));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionCreate, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadProgrammeCourses()
        {
            try
            {
                return ResponseData.SendSuccessMsg(data: _programmeCourseService.ReadProgrammeCourses());
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionReadAll, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadProgrammeCourse(long programmeCourseId)
        {
            try
            {
                var programmeCourse = _programmeCourseService.ReadProgrammeCourse(programmeCourseId);

                return programmeCourse != null
                    ? ResponseData.SendSuccessMsg(data: programmeCourse)
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureRead, ObjectName, programmeCourseId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionRead, ObjectName));
            }
        }

        public ActionResult<ResponseData> UpdateProgrammeCourse(long programmeCourseId, ProgrammeCourse programmeCourse)
        {
            try
            {
                if (programmeCourse == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));
                
                return ResponseData.SendSuccessMsg(_programmeCourseService.UpdateProgrammeCourse(programmeCourseId, programmeCourse) 
                    ? string.Format(DefaultConstants.SuccessfulUpdate, ObjectName, programmeCourseId) 
                    : string.Format(DefaultConstants.FailureUpdate, ObjectName, programmeCourseId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionUpdate, ObjectName, programmeCourseId));
            }
        }

        public ActionResult<ResponseData> DeleteProgrammeCourse(long programmeCourseId)
        {
            try
            {
                return ResponseData.SendSuccessMsg(_programmeCourseService.DeleteProgrammeCourse(programmeCourseId) 
                    ? string.Format(DefaultConstants.SuccessfulDelete, ObjectName, programmeCourseId) 
                    : string.Format(DefaultConstants.FailureDelete, ObjectName, programmeCourseId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionDelete, ObjectName, programmeCourseId));
            }
        }
    }
}