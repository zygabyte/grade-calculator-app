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

        public ActionResult<ResponseData> ReadSessionCourse(long sessionCourseId)
        {
            try
            {
                var sessionCourse = _sessionSemesterCourseService.ReadSessionCourse(sessionCourseId);

                return sessionCourse != null
                    ? ResponseData.SendSuccessMsg(data: sessionCourse)
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureRead, ObjectName, sessionCourseId));
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

        public ActionResult<ResponseData> DeleteSessionCourse(long sessionCourseId)
        {
            try
            {
                return ResponseData.SendSuccessMsg(_sessionSemesterCourseService.DeleteSessionCourse(sessionCourseId) 
                    ? string.Format(DefaultConstants.SuccessfulDelete, ObjectName, sessionCourseId) 
                    : string.Format(DefaultConstants.FailureDelete, ObjectName, sessionCourseId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionDelete, ObjectName, sessionCourseId));
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