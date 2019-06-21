using System;
using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers.Apis
{
    public class RegistrationCourseController : Controller
    {
        private readonly IRegistrationCourseService _registrationCourseService;
        private const string ObjectName = "RegistrationCourse"; 
        public RegistrationCourseController(IRegistrationCourseService registrationCourseService) => _registrationCourseService = registrationCourseService;
        
        public ActionResult<ResponseData> ReadRegistrationCourses(long sessionSemesterId, long programmeId, long studentId)
        {
            try
            {
                return ResponseData.SendSuccessMsg(data: _registrationCourseService.ReadRegistrationCourses(sessionSemesterId, programmeId, studentId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionReadAll, ObjectName));
            }
        }
    }
}