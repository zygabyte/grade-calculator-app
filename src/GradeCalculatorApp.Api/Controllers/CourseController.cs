using System;
using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Api.Controllers
{
    public class CourseController : Controller
    {

        private readonly ICourseService _courseService;
        private const string ObjectName = "Course"; 
        public CourseController(ICourseService courseService) => _courseService = courseService;
        
        // GET
//        public IActionResult Index()
//        {
//            return
//            View();
//        }

        public ActionResult<ResponseData> CreateCourse(Course course)
        {
            try
            {
                if (course == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));

                return ResponseData.SendSuccessMsg(_courseService.CreateCourse(course) 
                    ? string.Format(DefaultConstants.SuccessfulCreate, ObjectName) 
                    : string.Format(DefaultConstants.FailureCreate, ObjectName));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionCreate, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadCourses()
        {
            try
            {
                return ResponseData.SendSuccessMsg(data: _courseService.ReadCourses());
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionReadAll, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadCourse(long courseId)
        {
            try
            {
                var course = _courseService.ReadCourse(courseId);

                return course != null
                    ? ResponseData.SendSuccessMsg(data: course)
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureRead, ObjectName, courseId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionRead, ObjectName));
            }
        }

        public ActionResult<ResponseData> UpdateCourse(long courseId, Course course)
        {
            try
            {
                if (course == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));
                
                return ResponseData.SendSuccessMsg(_courseService.UpdateCourse(courseId, course) 
                    ? string.Format(DefaultConstants.SuccessfulUpdate, ObjectName, courseId) 
                    : string.Format(DefaultConstants.FailureUpdate, ObjectName, courseId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionUpdate, ObjectName, courseId));
            }
        }

        public ActionResult<ResponseData> DeleteCourse(long courseId)
        {
            try
            {
                return ResponseData.SendSuccessMsg(_courseService.DeleteCourse(courseId) 
                    ? string.Format(DefaultConstants.SuccessfulDelete, ObjectName, courseId) 
                    : string.Format(DefaultConstants.FailureDelete, ObjectName, courseId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionDelete, ObjectName, courseId));
            }
        }
    }
}