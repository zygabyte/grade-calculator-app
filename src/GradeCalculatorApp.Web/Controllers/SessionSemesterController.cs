using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class SessionSemesterController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}