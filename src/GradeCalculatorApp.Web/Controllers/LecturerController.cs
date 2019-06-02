using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class LecturerController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}