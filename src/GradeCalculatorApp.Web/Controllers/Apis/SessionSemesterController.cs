using System;
using System.Linq;
using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;
using GradeCalculatorApp.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers.Apis
{
    public class SessionSemesterController : Controller
    {

        private readonly ISessionSemesterService _sessionSemesterService;
        private const string ObjectName = "Session"; 
        public SessionSemesterController(ISessionSemesterService sessionSemesterService) => _sessionSemesterService = sessionSemesterService;
        
        public ActionResult<ResponseData> CreateSessionSemester(SessionSemester sessionSemester)
        {
            try
            {
                if (sessionSemester == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));
                
                if (sessionSemester.IsCurrent && _sessionSemesterService.CurrentExists()) return ResponseData.SendFailMsg(string.Format(DefaultConstants.CurrentExists));

                return _sessionSemesterService.CreateSessionSemester(sessionSemester)  
                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulCreate, ObjectName)) 
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureCreate, ObjectName));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionCreate, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadSessionSemesters()
        {
            try
            {
                return ResponseData.SendSuccessMsg(data: _sessionSemesterService.ReadSessionSemesters().Select(x => new SessionSemesterVm
                {
                    Id = x.Id, Courses = x.Courses, Semester = x.Semester.Name,
                    Session = x.Session.Name, SessionId = x.SessionId, SemesterId = x.SemesterId, 
                    SemesterStartDate = x.SemesterStartDate.ToString("yyyy-MM-dd"),
                    SemesterEndDate = x.SemesterEndDate.ToString("yyyy-MM-dd"),
                    IsCurrent = x.IsCurrent
                }));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionReadAll, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadSessionSemester(long sessionSemesterId)
        {
            try
            {
                var sessionSemester = _sessionSemesterService.ReadSessionSemester(sessionSemesterId);

                return sessionSemester != null
                    ? ResponseData.SendSuccessMsg(data: new SessionSemesterVm
                    {
                        Id = sessionSemester.Id, Courses = sessionSemester.Courses, Semester = sessionSemester.Semester.Name,
                        Session = sessionSemester.Session.Name, SessionId = sessionSemester.SessionId, SemesterId = sessionSemester.SemesterId,
                        SemesterStartDate = sessionSemester.SemesterStartDate.ToString("yyyy-MM-dd"),
                        SemesterEndDate = sessionSemester.SemesterEndDate.ToString("yyyy-MM-dd"),
                        IsCurrent = sessionSemester.IsCurrent
                    })
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureRead, ObjectName, sessionSemesterId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionRead, ObjectName));
            }
        }

        public ActionResult<ResponseData> UpdateSessionSemester(long sessionSemesterId, SessionSemester sessionSemester)
        {
            try
            {
                if (sessionSemester == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));
 
                if (sessionSemester.IsCurrent && _sessionSemesterService.CurrentExists()) return ResponseData.SendFailMsg(string.Format(DefaultConstants.CurrentExists));
                
                return _sessionSemesterService.UpdateSessionSemester(sessionSemesterId, sessionSemester)  
                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulUpdate, ObjectName, sessionSemesterId)) 
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureUpdate, ObjectName, sessionSemesterId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionUpdate, ObjectName, sessionSemesterId));
            }
        }

        public ActionResult<ResponseData> DeleteSessionSemester(long sessionSemesterId)
        {
            try
            {
                return _sessionSemesterService.DeleteSessionSemester(sessionSemesterId)  
                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulDelete, ObjectName, sessionSemesterId)) 
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureDelete, ObjectName, sessionSemesterId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionDelete, ObjectName, sessionSemesterId));
            }
        }
    }
}