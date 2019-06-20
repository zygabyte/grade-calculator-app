using GradeCalculatorApp.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class SessionController : BaseController
    {
        public SessionController(IAccountService accountService) : base(accountService){}
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}