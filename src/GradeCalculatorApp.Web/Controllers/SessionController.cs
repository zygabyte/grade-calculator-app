using System;
using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers
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

                return ResponseData.SendSuccessMsg(_sessionService.CreateSession(session) 
                    ? string.Format(DefaultConstants.SuccessfulCreate, ObjectName) 
                    : string.Format(DefaultConstants.FailureCreate, ObjectName));
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
                return ResponseData.SendSuccessMsg(data: _sessionService.ReadSessions());
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
                    ? ResponseData.SendSuccessMsg(data: session)
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
                
                return ResponseData.SendSuccessMsg(_sessionService.UpdateSession(sessionId, session) 
                    ? string.Format(DefaultConstants.SuccessfulUpdate, ObjectName, sessionId) 
                    : string.Format(DefaultConstants.FailureUpdate, ObjectName, sessionId));
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
                return ResponseData.SendSuccessMsg(_sessionService.DeleteSession(sessionId) 
                    ? string.Format(DefaultConstants.SuccessfulDelete, ObjectName, sessionId) 
                    : string.Format(DefaultConstants.FailureDelete, ObjectName, sessionId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionDelete, ObjectName, sessionId));
            }
        }
    }
}