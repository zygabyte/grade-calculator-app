using System;
using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Messaging;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class GradeCourseController : BaseController
    {
        private readonly ISessionSemesterService _sessionSemesterService;
        private readonly IGradeService _gradeService;
        private readonly IAccountService _accountService;
        private const string ObjectName = "Grade Course";

        private static long _gradedCourseId;

        public GradeCourseController(ISessionSemesterService sessionSemesterService, IGradeService gradeService, IAccountService accountService) : base(accountService)
        {
            _sessionSemesterService = sessionSemesterService;
            _gradeService = gradeService;
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            ViewBag.SessionSemester = _sessionSemesterService.ReadCurrentSessionSemester();
            ViewBag.User = _accountService.GetUserInSession();
            return View();
        }
        public IActionResult GradeDetails()
        {
            if (_gradedCourseId > 0)
            {
                ViewBag.GradedCourseId = _gradedCourseId;
                return View();   
            }

            return Unauthorized();
        }
        
        public ActionResult<ResponseData> SetSessionAndStudent(long gradedCourseId)
        {
            try
            {
                _gradedCourseId = gradedCourseId;
                return ResponseData.SendSuccessMsg();
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg();
            }
        }
        
        public ActionResult<ResponseData> ReadGradedCourse(long gradedCourseId)
        {
            try
            {
                return ResponseData.SendSuccessMsg(data: _gradeService.ReadGradedCourse(gradedCourseId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionRead, ObjectName, gradedCourseId));
            }
        }
    }
}