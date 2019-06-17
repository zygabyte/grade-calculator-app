using GradeCalculatorApp.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class GradeCourseController : Controller
    {
        private readonly ISessionSemesterService _sessionSemesterService;

        public GradeCourseController(ISessionSemesterService sessionSemesterService) =>
            _sessionSemesterService = sessionSemesterService;
        
        public IActionResult Index()
        {
            ViewBag.SessionSemester = _sessionSemesterService.ReadCurrentSessionSemester();
            return View();
        }
        public IActionResult GradeDetails()
        {
            ViewBag.SessionSemester = _sessionSemesterService.ReadCurrentSessionSemester();
            return View();
        }
    }
}