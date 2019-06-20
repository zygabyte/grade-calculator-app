using System.Diagnostics;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IAccountService _accountService;

        public HomeController(IAccountService accountService) : base(accountService) => _accountService = accountService;
        
        public IActionResult Index()
        {
            ViewBag.User = _accountService.GetUserInSession();
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