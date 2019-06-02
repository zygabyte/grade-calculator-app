using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class StudentController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}