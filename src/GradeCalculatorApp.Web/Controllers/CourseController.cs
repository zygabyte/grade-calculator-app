using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class CourseController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
        
        public IActionResult AddCourse()
        {
            return View();
        }
    }
}