using System;
using System.IO;
using System.Linq;
using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;
using GradeCalculatorApp.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers.Apis
{
    public class LecturerController : Controller
    {

        private readonly ILecturerService _lecturerService;
        private const string ObjectName = "Lecturer"; 
        public LecturerController(ILecturerService lecturerService) => _lecturerService = lecturerService;
        
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
                return ResponseData.SendSuccessMsg(data: _lecturerService.ReadLecturers().Select(x => new LecturerVm
                {
                    Id = x.Id, Department = x.Department.Name, DepartmentId = x.DepartmentId,
                    Email = x.Email, LastName = x.LastName, FirstName = x.FirstName
                }));
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
                    ? ResponseData.SendSuccessMsg(data: new LecturerVm
                    {
                        Id = lecturer.Id, Department = lecturer.Department.Name, DepartmentId = lecturer.DepartmentId,
                        Email = lecturer.Email, LastName = lecturer.LastName, FirstName = lecturer.FirstName
                    })
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
        
        public ActionResult<ResponseData> UploadLecturers()
        {
            try
            {
                var formFile = Request.Form.Files[0];
                if (formFile == null || formFile.Length == 0) return ResponseData.SendFailMsg(DefaultConstants.InvalidFileUpload);

                return _lecturerService.UploadLecturers(formFile);
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(DefaultConstants.ExceptionFileUpload);
            }
        }
        
        public IActionResult DownloadLecturerTemplate()
        {
            try
            {
                var fileModel = _lecturerService.DownloadLecturerTemplate();
                return File(fileModel.MemoryStream, fileModel.ContentType, Path.GetFileName(fileModel.Path));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return default;
            }
        }
    }
}