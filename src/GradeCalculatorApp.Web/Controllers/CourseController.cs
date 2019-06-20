using GradeCalculatorApp.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class CourseController : BaseController
    {
        public CourseController(IAccountService accountService) : base(accountService){}
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}