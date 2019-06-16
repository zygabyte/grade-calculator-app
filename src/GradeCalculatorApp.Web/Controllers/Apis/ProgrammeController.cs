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
    public class ProgrammeController : Controller
    {

        private readonly IProgrammeService _programmeService;
        private const string ObjectName = "Programme"; 
        public ProgrammeController(IProgrammeService programmeService) => _programmeService = programmeService;
        
        public ActionResult<ResponseData> CreateProgramme(Programme programme)
        {
            try
            {
                if (programme == null) return ResponseData.SendFailMsg(string.Format(DefaultConstants.InvalidObject, ObjectName));

                return _programmeService.CreateProgramme(programme)  
                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulCreate, ObjectName)) 
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureCreate, ObjectName));
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
                return ResponseData.SendSuccessMsg(data: _programmeService.ReadProgrammes().Select(x => new ProgrammeVm
                {
                    Id = x.Id, Name = x.Name, Code = x.Code,
                    Department = x.Department.Name, DepartmentId = x.DepartmentId
                }));
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
                    ? ResponseData.SendSuccessMsg(data: new ProgrammeVm
                    {
                        Id = programme.Id, Name = programme.Name, Code = programme.Code,
                        Department = programme.Department.Name, DepartmentId = programme.DepartmentId
                    })
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
                
                return _programmeService.UpdateProgramme(programmeId, programme)  
                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulUpdate, ObjectName, programmeId)) 
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureUpdate, ObjectName, programmeId));
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
                return _programmeService.DeleteProgramme(programmeId)  
                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulDelete, ObjectName, programmeId)) 
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureDelete, ObjectName, programmeId));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionDelete, ObjectName, programmeId));
            }
        }
    }
}