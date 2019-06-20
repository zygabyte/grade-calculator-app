using GradeCalculatorApp.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class SemesterController : BaseController
    {
        public SemesterController(IAccountService accountService) : base(accountService){}
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}