using System.Diagnostics;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.EnumLibrary;
using GradeCalculatorApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IDashboardService _dashboardService;
        private readonly ISessionSemesterService _sessionSemesterService;
        
        public HomeController(IAccountService accountService, IDashboardService dashboardService, ISessionSemesterService sessionSemesterService) : base(accountService)
        {
            _accountService = accountService;
            _dashboardService = dashboardService;
            _sessionSemesterService = sessionSemesterService;
        }

        public IActionResult Index()
        {
            var user = _accountService.GetUserInSession();
            var sessionSemester = _sessionSemesterService.ReadCurrentSessionSemester();

            switch (user.UserRole)
            {
                case UserRole.Lecturer when sessionSemester.Id > 0:
                    ViewBag.DashboardModel = _dashboardService.GetLecturerDashboard(sessionSemester.Id, user.Id);
                    break;
                case UserRole.Administrator:
                    ViewBag.DashboardModel = _dashboardService.GetAdminDashboard();
                    break;
            }

            ViewBag.SessionSemester = sessionSemester;
            ViewBag.User = user;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}