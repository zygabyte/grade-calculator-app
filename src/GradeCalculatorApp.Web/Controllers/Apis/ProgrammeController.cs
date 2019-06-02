using System;
using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers.Apis
{
    public class ProgrammeController : Controller
    {

        private readonly IProgrammeService _programmeService;
        private const string ObjectName = "Programme"; 
        public ProgrammeController(IProgrammeService programmeService) => _programmeService = programmeService;
        
        // GET
//        public IActionResult Index()
//        {
//            return
//            View();
//        }

        public ActionResult<ResponseData> CreateProgramme(Programme programme)
        {
            try
            {
                if (programme == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));

                return ResponseData.SendSuccessMsg(_programmeService.CreateProgramme(programme) 
                    ? string.Format(DefaultConstants.SuccessfulCreate, ObjectName) 
                    : string.Format(DefaultConstants.FailureCreate, ObjectName));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionCreate, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadProgrammes()
        {
            try
            {
                return ResponseData.SendSuccessMsg(data: _programmeService.ReadProgrammes());
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionReadAll, ObjectName));
            }
        }

        public ActionResult<ResponseData> ReadProgramme(long programmeId)
        {
            try
            {
                var programme = _programmeService.ReadProgramme(programmeId);

                return programme != null
                    ? ResponseData.SendSuccessMsg(data: programme)
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureRead, ObjectName, programmeId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionRead, ObjectName));
            }
        }

        public ActionResult<ResponseData> UpdateProgramme(long programmeId, Programme programme)
        {
            try
            {
                if (programme == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));
                
                return ResponseData.SendSuccessMsg(_programmeService.UpdateProgramme(programmeId, programme) 
                    ? string.Format(DefaultConstants.SuccessfulUpdate, ObjectName, programmeId) 
                    : string.Format(DefaultConstants.FailureUpdate, ObjectName, programmeId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionUpdate, ObjectName, programmeId));
            }
        }

        public ActionResult<ResponseData> DeleteProgramme(long programmeId)
        {
            try
            {
                return ResponseData.SendSuccessMsg(_programmeService.DeleteProgramme(programmeId) 
                    ? string.Format(DefaultConstants.SuccessfulDelete, ObjectName, programmeId) 
                    : string.Format(DefaultConstants.FailureDelete, ObjectName, programmeId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionDelete, ObjectName, programmeId));
            }
        }
    }
}