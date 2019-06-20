using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
{
    public class ProgrammeController : BaseController
    {
        private static long _programmeId;
        private readonly IProgrammeService _programmeService;
        
        public ProgrammeController(IProgrammeService programmeService, IAccountService accountService) : base(accountService) => _programmeService = programmeService;
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
        
        public IActionResult ProgrammeCourses()
        {
            if (_programmeId > 0)
            {
                var programme = _programmeService.ReadProgramme(_programmeId);
                ViewBag.ProgrammeId = _programmeId;
                ViewBag.ProgrammeFullName = $"{programme.Name}";
                return View();
            } 
            return Unauthorized();
        }
        // GET
        public ActionResult<ResponseData> SetProgrammeId(long programmeId)
        {
            if (programmeId > 0)
            {
                _programmeId = programmeId;
                return ResponseData.SendSuccessMsg();
            }

            return ResponseData.SendFailMsg(DefaultConstants.InvalidId);
        }

        public IActionResult AddProgrammeCourse()
        {
            if (_programmeId > 0)
            {
                ViewBag.ProgrammeId = _programmeId;
                return View();
            } 
            return Unauthorized();
        }
    }
}