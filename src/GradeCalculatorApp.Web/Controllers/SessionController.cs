using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class SessionController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}