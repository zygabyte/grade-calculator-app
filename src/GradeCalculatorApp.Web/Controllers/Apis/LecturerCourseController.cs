using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers.Apis
{
    public class LecturerCourseController : Controller
    {

        private readonly ILecturerCourseService _lecturerCourseService;
        private const string ObjectName = "LecturerCourse"; 
        private const string Courses = "Courses"; 
        private const string Lecturer = "Lecturer"; 
        
        public LecturerCourseController(ILecturerCourseService lecturerCourseService) => _lecturerCourseService = lecturerCourseService;

        public ActionResult<ResponseData> CreateLecturerCourse(LecturerCourse lecturerCourse)
        {
            try
            {
                if (lecturerCourse == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));

                return ResponseData.SendSuccessMsg(_lecturerCourseService.CreateLecturerCourse(lecturerCourse) 
                    ? string.Format(DefaultConstants.SuccessfulCreate, ObjectName) 
                    : string.Format(DefaultConstants.FailureCreate, ObjectName));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionCreate, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadLecturerCourses()
        {
            try
            {
                return ResponseData.SendSuccessMsg(data: _lecturerCourseService.ReadLecturerCourses());
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionReadAll, ObjectName));
            }
        }
        
        public ActionResult<ResponseData> ReadUniqueLecturerCourses(long lecturerId)
        {
            try
            {
                return ResponseData.SendSuccessMsg(data: _lecturerCourseService.ReadUniqueLecturerCourses(lecturerId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionReadAll, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadLecturerCourse(long lecturerId)
        {
            try
            {
                var lecturerCourse = _lecturerCourseService.ReadLecturerCourse(lecturerId);

                return lecturerCourse != null
                    ? ResponseData.SendSuccessMsg(data: lecturerCourse)
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureRead, ObjectName, lecturerId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionRead, ObjectName));
            }
        }

        public ActionResult<ResponseData> UpdateLecturerCourse(long lecturerCourseId, LecturerCourse lecturerCourse)
        {
            try
            {
                if (lecturerCourse == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));
                
                return ResponseData.SendSuccessMsg(_lecturerCourseService.UpdateLecturerCourse(lecturerCourseId, lecturerCourse) 
                    ? string.Format(DefaultConstants.SuccessfulUpdate, ObjectName, lecturerCourseId) 
                    : string.Format(DefaultConstants.FailureUpdate, ObjectName, lecturerCourseId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionUpdate, ObjectName, lecturerCourseId));
            }
        }

        public ActionResult<ResponseData> DeleteLecturerCourse(long lecturerId, long courseId)
        {
            try
            {
                return _lecturerCourseService.DeleteLecturerCourse(lecturerId, courseId)  
                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulDelete, ObjectName, lecturerId)) 
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureDelete, ObjectName, lecturerId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionDelete, ObjectName, lecturerId));
            }
        }

        public ActionResult<ResponseData> MapCourses(long lecturerId, List<long> courseIds)
        {
            try
            {
                return _lecturerCourseService.MapCourses(lecturerId, courseIds)
                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulMap, Courses, Lecturer))
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.SuccessfulMap, Courses, Lecturer));

            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionMap, Courses, Lecturer));
            }
        }
    }
}