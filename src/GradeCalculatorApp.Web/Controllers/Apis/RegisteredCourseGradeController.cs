using System;
using System.Collections.Generic;
using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers.Apis
{
    public class RegisteredCourseGradeController : Controller
    {
        private readonly IRegisteredCourseGradeService _registeredCourseGradeService;
        private const string ObjectName = "Registered Course"; 
        
        public RegisteredCourseGradeController(IRegisteredCourseGradeService registeredCourseGradeService) => _registeredCourseGradeService = registeredCourseGradeService;
        
        public ActionResult<ResponseData> CreateRegisteredCourseGrade(RegisteredCourseGrade registeredCourseGrade)
        {
            try
            {
                if (registeredCourseGrade == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));

                return _registeredCourseGradeService.CreateRegisteredCourseGrades(registeredCourseGrade) 
                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulCreate, ObjectName)) 
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureCreate, ObjectName));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionCreate, ObjectName));
            }
        }
    }
}