using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class GradeCourseController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}