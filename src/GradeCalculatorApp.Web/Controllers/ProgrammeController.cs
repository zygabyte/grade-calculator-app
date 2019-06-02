using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class ProgrammeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}