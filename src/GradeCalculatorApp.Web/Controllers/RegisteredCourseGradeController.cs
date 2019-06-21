using GradeCalculatorApp.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class RegisteredCourseGradeController : BaseController
    {
        private readonly ISessionSemesterService _sessionSemesterService;
        private readonly IAccountService _accountService;

        public RegisteredCourseGradeController(ISessionSemesterService sessionSemesterService, IAccountService accountService) : base(accountService)
        {
            _sessionSemesterService = sessionSemesterService;
            _accountService = accountService;
        }

        // GET
        public IActionResult Index()
        {
            ViewBag.SessionSemester = _sessionSemesterService.ReadCurrentSessionSemester();
            ViewBag.User = _accountService.GetUserInSession();
            return View();
        }
    }
}