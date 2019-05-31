using System;
using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Api.Controllers
{
    public class StudentController : Controller
    {

        private readonly IStudentService _studentService;
        private const string ObjectName = "Student"; 
        public StudentController(IStudentService studentService) => _studentService = studentService;
        
        // GET
//        public IActionResult Index()
//        {
//            return
//            View();
//        }

        public ActionResult<ResponseData> CreateStudent(Student student)
        {
            try
            {
                if (student == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));

                return ResponseData.SendSuccessMsg(_studentService.CreateStudent(student) 
                    ? string.Format(DefaultConstants.SuccessfulCreate, ObjectName) 
                    : string.Format(DefaultConstants.FailureCreate, ObjectName));
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
                return ResponseData.SendSuccessMsg(data: _studentService.ReadStudents());
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
                    ? ResponseData.SendSuccessMsg(data: student)
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
                
                return ResponseData.SendSuccessMsg(_studentService.UpdateStudent(studentId, student) 
                    ? string.Format(DefaultConstants.SuccessfulUpdate, ObjectName, studentId) 
                    : string.Format(DefaultConstants.FailureUpdate, ObjectName, studentId));
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
                return ResponseData.SendSuccessMsg(_studentService.DeleteStudent(studentId) 
                    ? string.Format(DefaultConstants.SuccessfulDelete, ObjectName, studentId) 
                    : string.Format(DefaultConstants.FailureDelete, ObjectName, studentId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionDelete, ObjectName, studentId));
            }
        }
    }
}