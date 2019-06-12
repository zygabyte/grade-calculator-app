using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class LecturerController : Controller
    {
        private static long _lecturerId;
        private readonly ILecturerService _lecturerService;

        public LecturerController(ILecturerService lecturerService) => _lecturerService = lecturerService;
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
        
        public IActionResult LecturerCourses()
        {
            if (_lecturerId > 0)
            {
                var lecturer = _lecturerService.ReadLecturer(_lecturerId);
                ViewBag.LecturerId = _lecturerId;
                ViewBag.LecturerFullName = $"{lecturer.FirstName} {lecturer.LastName}";
                return View();
            } 
            return Unauthorized();
        }
        // GET
        public ActionResult<ResponseData> SetLecturerId(long lecturerId)
        {
            if (lecturerId > 0)
            {
                _lecturerId = lecturerId;
                return ResponseData.SendSuccessMsg();
            }

            return ResponseData.SendFailMsg(DefaultConstants.InvalidId);
        }

        public IActionResult AddLecturerCourse()
        {
            if (_lecturerId > 0)
            {
                ViewBag.LecturerId = _lecturerId;
                return View();
            } 
            return Unauthorized();
        }
    }
}