using GradeCalculatorApp.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class RegisteredCourseGradeController : Controller
    {
        private readonly ISessionSemesterService _sessionSemesterService;

        public RegisteredCourseGradeController(ISessionSemesterService sessionSemesterService) =>
            _sessionSemesterService = sessionSemesterService;
        
        // GET
        public IActionResult Index()
        {
            ViewBag.SessionSemester = _sessionSemesterService.ReadCurrentSessionSemester();
            return View();
        }
    }
}