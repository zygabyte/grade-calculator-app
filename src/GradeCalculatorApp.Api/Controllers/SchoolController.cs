using System;
using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Api.Controllers
{
    public class SchoolController : Controller
    {

        private readonly ISchoolService _schoolService;
        private const string ObjectName = "School"; 
        public SchoolController(ISchoolService schoolService) => _schoolService = schoolService;
        
        // GET
//        public IActionResult Index()
//        {
//            return
//            View();
//        }

        public ActionResult<ResponseData> CreateSchool(School school)
        {
            try
            {
                if (school == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));

                return ResponseData.SendSuccessMsg(_schoolService.CreateSchool(school) 
                    ? string.Format(DefaultConstants.SuccessfulCreate, ObjectName) 
                    : string.Format(DefaultConstants.FailureCreate, ObjectName));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionCreate, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadSchools()
        {
            try
            {
                return ResponseData.SendSuccessMsg(data: _schoolService.ReadSchools());
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionReadAll, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadSchool(long schoolId)
        {
            try
            {
                var school = _schoolService.ReadSchool(schoolId);

                return school != null
                    ? ResponseData.SendSuccessMsg(data: school)
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureRead, ObjectName, schoolId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionRead, ObjectName));
            }
        }

        public ActionResult<ResponseData> UpdateSchool(long schoolId, School school)
        {
            try
            {
                if (school == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));
                
                return ResponseData.SendSuccessMsg(_schoolService.UpdateSchool(schoolId, school) 
                    ? string.Format(DefaultConstants.SuccessfulUpdate, ObjectName, schoolId) 
                    : string.Format(DefaultConstants.FailureUpdate, ObjectName, schoolId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionUpdate, ObjectName, schoolId));
            }
        }

        public ActionResult<ResponseData> DeleteSchool(long schoolId)
        {
            try
            {
                return ResponseData.SendSuccessMsg(_schoolService.DeleteSchool(schoolId) 
                    ? string.Format(DefaultConstants.SuccessfulDelete, ObjectName, schoolId) 
                    : string.Format(DefaultConstants.FailureDelete, ObjectName, schoolId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionDelete, ObjectName, schoolId));
            }
        }
    }
}