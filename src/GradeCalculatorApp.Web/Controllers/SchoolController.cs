using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class SchoolController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}