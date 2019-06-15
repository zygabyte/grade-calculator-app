using System;
using System.Collections.Generic;
using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers.Apis
{
    public class ProgrammeCourseController : Controller
    {

        private readonly IProgrammeCourseService _programmeCourseService;
        private const string ObjectName = "ProgrammeCourse"; 
        private const string Courses = "Courses"; 
        private const string Programme = "Programme"; 
        
        public ProgrammeCourseController(IProgrammeCourseService programmeCourseService) => _programmeCourseService = programmeCourseService;
        
        public ActionResult<ResponseData> CreateProgrammeCourse(ProgrammeCourse programmeCourse)
        {
            try
            {
                if (programmeCourse == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));

                return ResponseData.SendSuccessMsg(_programmeCourseService.CreateProgrammeCourse(programmeCourse) 
                    ? string.Format(DefaultConstants.SuccessfulCreate, ObjectName) 
                    : string.Format(DefaultConstants.FailureCreate, ObjectName));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionCreate, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadProgrammeCourses()
        {
            try
            {
                return ResponseData.SendSuccessMsg(data: _programmeCourseService.ReadProgrammeCourses());
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionReadAll, ObjectName));
            }
        }
        
        public ActionResult<ResponseData> ReadUniqueProgrammeCourses(long programmeId)
        {
            try
            {
                return ResponseData.SendSuccessMsg(data: _programmeCourseService.ReadUniqueProgrammeCourses(programmeId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionReadAll, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadProgrammeCourse(long programmeId)
        {
            try
            {
                var programmeCourse = _programmeCourseService.ReadProgrammeCourse(programmeId);

                return programmeCourse != null
                    ? ResponseData.SendSuccessMsg(data: programmeCourse)
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureRead, ObjectName, programmeId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionRead, ObjectName));
            }
        }

        public ActionResult<ResponseData> UpdateProgrammeCourse(long programmeCourseId, ProgrammeCourse programmeCourse)
        {
            try
            {
                if (programmeCourse == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));
                
                return ResponseData.SendSuccessMsg(_programmeCourseService.UpdateProgrammeCourse(programmeCourseId, programmeCourse) 
                    ? string.Format(DefaultConstants.SuccessfulUpdate, ObjectName, programmeCourseId) 
                    : string.Format(DefaultConstants.FailureUpdate, ObjectName, programmeCourseId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionUpdate, ObjectName, programmeCourseId));
            }
        }

        public ActionResult<ResponseData> DeleteProgrammeCourse(long programmeId, long courseId)
        {
            try
            {
                return ResponseData.SendSuccessMsg(_programmeCourseService.DeleteProgrammeCourse(programmeId, courseId) 
                    ? string.Format(DefaultConstants.SuccessfulDelete, ObjectName, programmeId) 
                    : string.Format(DefaultConstants.FailureDelete, ObjectName, programmeId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionDelete, ObjectName, programmeId));
            }
        }
        
        public ActionResult<ResponseData> MapCourses(long programmeId, List<long> courseIds)
        {
            try
            {
                return _programmeCourseService.MapCourses(programmeId, courseIds)
                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulMap, Courses, Programme))
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.SuccessfulMap, Courses, Programme));

            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionMap, Courses, Programme));
            }
        }
    }
}