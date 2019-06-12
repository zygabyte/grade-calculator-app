using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class SessionSemesterController : Controller
    {
        private static long _sessionSemesterId;
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
        // GET
        public IActionResult SessionSemesterCourses()
        {
            if (_sessionSemesterId > 0)
            {
                ViewBag.SessionSemesterId = _sessionSemesterId;
                return View();
            } 
            return Unauthorized();
        }
        // GET
        public ActionResult<ResponseData> SetSessionSemesterId(long sessionSemesterId)
        {
            if (sessionSemesterId > 0)
            {
                _sessionSemesterId = sessionSemesterId;
                return ResponseData.SendSuccessMsg();
            }

            return ResponseData.SendFailMsg(DefaultConstants.InvalidId);
        }

        public IActionResult AddSessionSemesterCourse()
        {
            if (_sessionSemesterId > 0)
            {
                ViewBag.SessionSemesterId = _sessionSemesterId;
                return View();
            } 
            return Unauthorized();
        }
    }
}