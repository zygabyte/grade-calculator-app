using System;
using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Messaging;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class GradeCourseController : Controller
    {
        private readonly ISessionSemesterService _sessionSemesterService;
        private readonly IMessagingService _messagingService;
        private readonly IGradeService _gradeService;
        private const string ObjectName = "Grade Course";

        private static long _gradedCourseId;

        public GradeCourseController(ISessionSemesterService sessionSemesterService, IGradeService gradeService, IMessagingService messagingService)
        {
            _sessionSemesterService = sessionSemesterService;
            _gradeService = gradeService;
            _messagingService = messagingService;
        }

        public IActionResult Index()
        {
            _messagingService.SendMail("eseosaehioghae@gmail.com", "", "", "Heading", "message", "");
            ViewBag.SessionSemester = _sessionSemesterService.ReadCurrentSessionSemester();
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