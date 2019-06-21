using GradeCalculatorApp.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class RegisterCourseController : BaseController
    {
        private readonly ISessionSemesterService _sessionSemesterService;
        private readonly IAccountService _accountService;
        private readonly IStudentService _studentService;

        public RegisterCourseController(ISessionSemesterService sessionSemesterService, IAccountService accountService, IStudentService studentService) : base(accountService)
        {
            _sessionSemesterService = sessionSemesterService;
            _accountService = accountService;
            _studentService = studentService;
        }

        // GET
        public IActionResult Index()
        {
            var user = _accountService.GetUserInSession();
            ViewBag.SessionSemester = _sessionSemesterService.ReadCurrentSessionSemester();
            ViewBag.User = user;
            ViewBag.ProgrammeId = _studentService.ReadStudentByEmail(user?.Email)?.ProgrammeId;
            return View();
        }
    }
}