using System;
using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers.Apis
{
    public class LecturerController : Controller
    {

        private readonly ILecturerService _lecturerService;
        private const string ObjectName = "Lecturer"; 
        public LecturerController(ILecturerService lecturerService) => _lecturerService = lecturerService;
        
        // GET
//        public IActionResult Index()
//        {
//            return
//            View();
//        }

        public ActionResult<ResponseData> CreateLecturer(Lecturer lecturer)
        {
            try
            {
                if (lecturer == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));

                return _lecturerService.CreateLecturer(lecturer)  
                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulCreate, ObjectName)) 
                    : ResponseData.SendSuccessMsg(string.Format(DefaultConstants.FailureCreate, ObjectName));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionCreate, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadLecturers()
        {
            try
            {
                return ResponseData.SendSuccessMsg(data: _lecturerService.ReadLecturers());
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionReadAll, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadLecturer(long lecturerId)
        {
            try
            {
                var lecturer = _lecturerService.ReadLecturer(lecturerId);

                return lecturer != null
                    ? ResponseData.SendSuccessMsg(data: lecturer)
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureRead, ObjectName, lecturerId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionRead, ObjectName));
            }
        }

        public ActionResult<ResponseData> UpdateLecturer(long lecturerId, Lecturer lecturer)
        {
            try
            {
                if (lecturer == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));
                
                return _lecturerService.UpdateLecturer(lecturerId, lecturer)  
                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulUpdate, ObjectName, lecturerId))
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureUpdate, ObjectName, lecturerId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionUpdate, ObjectName, lecturerId));
            }
        }

        public ActionResult<ResponseData> DeleteLecturer(long lecturerId)
        {
            try
            {
                return _lecturerService.DeleteLecturer(lecturerId)  
                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulDelete, ObjectName, lecturerId)) 
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureDelete, ObjectName, lecturerId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionDelete, ObjectName, lecturerId));
            }
        }
    }
}