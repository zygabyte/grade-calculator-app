using System;
using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers.Apis
{
    public class GradeCourseController : Controller
    {
        private readonly IGradeService _gradeService;
        private const string ObjectName = "Grade Course"; 
        public GradeCourseController(IGradeService gradeService) => _gradeService = gradeService;
        
        public ActionResult<ResponseData> ReadGradedCourses(long sessionSemesterId, long studentId)
        {
            try
            {
                return ResponseData.SendSuccessMsg(data: _gradeService.ReadGradedCourses(sessionSemesterId, studentId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionReadAll, ObjectName));
            }
        }
    }
}