using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class DepartmentController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}