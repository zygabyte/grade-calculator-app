using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class LogInController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}