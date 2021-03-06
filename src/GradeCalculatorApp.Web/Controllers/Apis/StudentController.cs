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
    public class StudentController : Controller
    {

        private readonly IStudentService _studentService;
        private const string ObjectName = "Student"; 
        public StudentController(IStudentService studentService) => _studentService = studentService;
 
        public ActionResult<ResponseData> CreateStudent(Student student)
        {
            try
            {
                if (student == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));

                return _studentService.CreateStudent(student)  
                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulCreate, ObjectName)) 
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureCreate, ObjectName));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionCreate, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadStudents()
        {
            try
            {
                return ResponseData.SendSuccessMsg(data: _studentService.ReadStudents().Select(x => new StudentVm
                {
                    Id = x.Id, Email = x.Email, MatricNumber = x.MatricNumber, Programme = x.Programme.Name,
                    FirstName = x.FirstName, LastName = x.LastName, ProgrammeId = x.ProgrammeId
                }));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionReadAll, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadStudent(long studentId)
        {
            try
            {
                var student = _studentService.ReadStudent(studentId);

                return student != null
                    ? ResponseData.SendSuccessMsg(data: new StudentVm
                    {
                        Id = student.Id, Email = student.Email, MatricNumber = student.MatricNumber, Programme = student.Programme.Name,
                        FirstName = student.FirstName, LastName = student.LastName, ProgrammeId = student.ProgrammeId
                    })
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureRead, ObjectName, studentId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionRead, ObjectName));
            }
        }

        public ActionResult<ResponseData> UpdateStudent(long studentId, Student student)
        {
            try
            {
                if (student == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));
                
                return _studentService.UpdateStudent(studentId, student)  
                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulUpdate, ObjectName, studentId)) 
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureUpdate, ObjectName, studentId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionUpdate, ObjectName, studentId));
            }
        }

        public ActionResult<ResponseData> DeleteStudent(long studentId)
        {
            try
            {
                return _studentService.DeleteStudent(studentId)  
                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulDelete, ObjectName, studentId)) 
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureDelete, ObjectName, studentId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionDelete, ObjectName, studentId));
            }
        }
        
        public ActionResult<ResponseData> UploadStudents()
        {
            try
            {
                var formFile = Request.Form.Files[0];
                if (formFile == null || formFile.Length == 0) return ResponseData.SendFailMsg(DefaultConstants.InvalidFileUpload);

                return _studentService.UploadStudents(formFile);
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(DefaultConstants.ExceptionFileUpload);
            }
        }
        
        public IActionResult DownloadStudentTemplate()
        {
            try
            {
                var fileModel = _studentService.DownloadStudentTemplate();
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