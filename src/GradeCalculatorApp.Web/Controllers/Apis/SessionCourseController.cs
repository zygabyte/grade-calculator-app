using System;
using System.Collections.Generic;
using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers.Apis
{
    public class SessionCourseController : Controller
    {
        private readonly ISessionSemesterCourseService _sessionSemesterCourseService;
        private const string ObjectName = "SessionCourse"; 
        private const string Courses = "Courses"; 
        private const string SessionSemester = "Session Semester"; 
        
        public SessionCourseController(ISessionSemesterCourseService sessionSemesterCourseService) => _sessionSemesterCourseService = sessionSemesterCourseService;

        public ActionResult<ResponseData> CreateSessionCourse(SessionSemesterCourse sessionSemesterCourse)
        {
            try
            {
                if (sessionSemesterCourse == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));

                return ResponseData.SendSuccessMsg(_sessionSemesterCourseService.CreateSessionCourse(sessionSemesterCourse) 
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
                return ResponseData.SendSuccessMsg(data: _sessionSemesterCourseService.ReadSessionCourses());
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionReadAll, ObjectName));
            }
        }


        public ActionResult<ResponseData> ReadUniqueSessionCourses(long sessionSemesterId)
        {
            try
            {
                return ResponseData.SendSuccessMsg(data: _sessionSemesterCourseService.ReadUniqueSessionCourses(sessionSemesterId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionReadAll, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadSessionCourse(long sessionSemesterId)
        {
            try
            {
                var sessionCourse = _sessionSemesterCourseService.ReadSessionCourse(sessionSemesterId);

                return sessionCourse != null
                    ? ResponseData.SendSuccessMsg(data: sessionCourse)
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureRead, ObjectName, sessionSemesterId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionRead, ObjectName));
            }
        }

        public ActionResult<ResponseData> UpdateSessionCourse(long sessionCourseId, SessionSemesterCourse sessionSemesterCourse)
        {
            try
            {
                if (sessionSemesterCourse == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));
                
                return ResponseData.SendSuccessMsg(_sessionSemesterCourseService.UpdateSessionCourse(sessionCourseId, sessionSemesterCourse) 
                    ? string.Format(DefaultConstants.SuccessfulUpdate, ObjectName, sessionCourseId) 
                    : string.Format(DefaultConstants.FailureUpdate, ObjectName, sessionCourseId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionUpdate, ObjectName, sessionCourseId));
            }
        }

        public ActionResult<ResponseData> DeleteSessionCourse(long sessionSemesterId, long courseId)
        {
            try
            {
                return _sessionSemesterCourseService.DeleteSessionCourse(sessionSemesterId, courseId)  
                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulDelete, ObjectName, sessionSemesterId)) 
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureDelete, ObjectName, sessionSemesterId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionDelete, ObjectName, sessionSemesterId));
            }
        }
        
        public ActionResult<ResponseData> MapCourses(long sessionSemesterId, IEnumerable<long> courseIds)
        {
            try
            {
                return _sessionSemesterCourseService.MapCourses(sessionSemesterId, courseIds)
                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulMap, Courses, SessionSemester))
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.SuccessfulMap, Courses, SessionSemester));

            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionMap, Courses, SessionSemester));
            }
        }
    }
}