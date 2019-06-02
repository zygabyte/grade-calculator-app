using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class SemesterController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}