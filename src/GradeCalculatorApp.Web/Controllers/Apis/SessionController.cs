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
    public class SessionController : Controller
    {

        private readonly ISessionService _sessionService;
        private const string ObjectName = "Session"; 
        public SessionController(ISessionService sessionService) => _sessionService = sessionService;
        
        // GET
//        public IActionResult Index()
//        {
//            return
//            View();
//        }

        public ActionResult<ResponseData> CreateSession(Session session)
        {
            try
            {
                if (session == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));

                return _sessionService.CreateSession(session)  
                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulCreate, ObjectName)) 
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureCreate, ObjectName));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionCreate, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadSessions()
        {
            try
            {
                return ResponseData.SendSuccessMsg(data: _sessionService.ReadSessions().Select(x => new SessionVm
                {
                    Id = x.Id, Courses = x.Courses, Semester = x.Semester.Name,
                    Name = x.Name, SemesterId = x.SemesterId, 
                    SemesterStartDate = x.SemesterStartDate.ToString("yyyy-MM-dd"),
                    SemesterEndDate = x.SemesterEndDate.ToString("yyyy-MM-dd")
                }));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionReadAll, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadSession(long sessionId)
        {
            try
            {
                var session = _sessionService.ReadSession(sessionId);

                return session != null
                    ? ResponseData.SendSuccessMsg(data: new SessionVm
                    {
                        Id = session.Id, Courses = session.Courses, Semester = session.Semester.Name,
                        Name = session.Name, SemesterId = session.SemesterId,
                        SemesterStartDate = session.SemesterStartDate.ToString("yyyy-MM-dd"),
                        SemesterEndDate = session.SemesterEndDate.ToString("yyyy-MM-dd")
                    })
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureRead, ObjectName, sessionId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionRead, ObjectName));
            }
        }

        public ActionResult<ResponseData> UpdateSession(long sessionId, Session session)
        {
            try
            {
                if (session == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));
                
                return _sessionService.UpdateSession(sessionId, session)  
                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulUpdate, ObjectName, sessionId)) 
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureUpdate, ObjectName, sessionId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionUpdate, ObjectName, sessionId));
            }
        }

        public ActionResult<ResponseData> DeleteSession(long sessionId)
        {
            try
            {
                return _sessionService.DeleteSession(sessionId)  
                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulDelete, ObjectName, sessionId)) 
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureDelete, ObjectName, sessionId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionDelete, ObjectName, sessionId));
            }
        }
    }
}