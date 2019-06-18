using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET
        public IActionResult LogIn()
        {
            return
            View();
        }
        
        public IActionResult MailTemplate()
        {
            return
            View();
        }
    }
}