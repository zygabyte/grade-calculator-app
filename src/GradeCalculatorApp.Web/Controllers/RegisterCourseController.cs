using GradeCalculatorApp.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class RegisterCourseController : Controller
    {
        private readonly ISessionSemesterService _sessionSemesterService;

        public RegisterCourseController(ISessionSemesterService sessionSemesterService) =>
            _sessionSemesterService = sessionSemesterService;
        
        // GET
        public IActionResult Index()
        {
            ViewBag.SessionSemester = _sessionSemesterService.ReadCurrentSessionSemester();
            return View();
        }
    }
}