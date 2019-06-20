using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.EnumLibrary;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IAccountService _accountService;

        public AccountController(ITokenService tokenService, IAccountService accountService)
        {
            _tokenService = tokenService;
            _accountService = accountService;
        }

        public IActionResult LogIn()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            _accountService.ClearSession();
            return RedirectToAction("LogIn");
        }
        
        public IActionResult Register(string id)
        {
            var tokenMap = _tokenService.ReadUserByTokenMap(id);

            switch (tokenMap?.UserRole)
            {
                case UserRole.Student:
                    ViewBag.User = _tokenService.ReadStudentByEmail(tokenMap.Email);
                    break;
                
                case UserRole.Lecturer:
                    ViewBag.User = _tokenService.ReadLecturerByEmail(tokenMap.Email);
                    break;
            }
            
            return View();
        }
        
        public IActionResult MailTemplate()
        {
            return
            View();
        }
    }
}