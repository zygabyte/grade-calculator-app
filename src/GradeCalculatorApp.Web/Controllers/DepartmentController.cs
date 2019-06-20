using GradeCalculatorApp.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class DepartmentController : BaseController
    {
        public DepartmentController(IAccountService accountService) : base(accountService){}
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}