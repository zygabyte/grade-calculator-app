using System;
using System.Collections.Generic;
using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers.Apis
{
    public class RegisterCourseController : Controller
    {
        private readonly IRegisteredCourseService _semesterService;
        private const string ObjectName = "RegisteredCourse"; 
        public RegisterCourseController(IRegisteredCourseService semesterService) => _semesterService = semesterService;

        public ActionResult<ResponseData> CreateRegisterCourse(List<RegisteredCourse> registeredCourses)
        {
            try
            {
                if (registeredCourses == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));

                return _semesterService.CreateRegisteredCourses(registeredCourses) 
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