using System;
using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers.Apis
{
    public class SessionCourseController : Controller
    {

        private readonly ISessionCourseService _sessionCourseService;
        private const string ObjectName = "SessionCourse"; 
        public SessionCourseController(ISessionCourseService sessionCourseService) => _sessionCourseService = sessionCourseService;
        
        // GET
//        public IActionResult Index()
//        {
//            return
//            View();
//        }

        public ActionResult<ResponseData> CreateSessionCourse(SessionCourse sessionCourse)
        {
            try
            {
                if (sessionCourse == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));

                return ResponseData.SendSuccessMsg(_sessionCourseService.CreateSessionCourse(sessionCourse) 
                    ? string.Format(DefaultConstants.SuccessfulCreate, ObjectName) 
                    : string.Format(DefaultConstants.FailureCreate, ObjectName));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionCreate, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadSessionCourses()
        {
            try
            {
                return ResponseData.SendSuccessMsg(data: _sessionCourseService.ReadSessionCourses());
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionReadAll, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadSessionCourse(long sessionCourseId)
        {
            try
            {
                var sessionCourse = _sessionCourseService.ReadSessionCourse(sessionCourseId);

                return sessionCourse != null
                    ? ResponseData.SendSuccessMsg(data: sessionCourse)
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureRead, ObjectName, sessionCourseId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionRead, ObjectName));
            }
        }

        public ActionResult<ResponseData> UpdateSessionCourse(long sessionCourseId, SessionCourse sessionCourse)
        {
            try
            {
                if (sessionCourse == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));
                
                return ResponseData.SendSuccessMsg(_sessionCourseService.UpdateSessionCourse(sessionCourseId, sessionCourse) 
                    ? string.Format(DefaultConstants.SuccessfulUpdate, ObjectName, sessionCourseId) 
                    : string.Format(DefaultConstants.FailureUpdate, ObjectName, sessionCourseId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionUpdate, ObjectName, sessionCourseId));
            }
        }

        public ActionResult<ResponseData> DeleteSessionCourse(long sessionCourseId)
        {
            try
            {
                return ResponseData.SendSuccessMsg(_sessionCourseService.DeleteSessionCourse(sessionCourseId) 
                    ? string.Format(DefaultConstants.SuccessfulDelete, ObjectName, sessionCourseId) 
                    : string.Format(DefaultConstants.FailureDelete, ObjectName, sessionCourseId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionDelete, ObjectName, sessionCourseId));
            }
        }
    }
}