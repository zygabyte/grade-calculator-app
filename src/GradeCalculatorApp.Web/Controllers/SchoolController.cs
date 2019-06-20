using GradeCalculatorApp.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class SchoolController : BaseController
    {
        public SchoolController(IAccountService accountService) : base(accountService){}
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}