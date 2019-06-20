using GradeCalculatorApp.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class RegisterCourseController : BaseController
    {
        private readonly ISessionSemesterService _sessionSemesterService;

        public RegisterCourseController(ISessionSemesterService sessionSemesterService, IAccountService accountService) : base(accountService) =>
            _sessionSemesterService = sessionSemesterService;
        
        // GET
        public IActionResult Index()
        {
            ViewBag.SessionSemester = _sessionSemesterService.ReadCurrentSessionSemester();
            return View();
        }
    }
}