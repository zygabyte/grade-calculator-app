using System;
using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;
using GradeCalculatorApp.EnumLibrary;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Web.Controllers.Apis
{
    public class AccountController : Controller
    {
        
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService) => _accountService = accountService;

        public ActionResult<ResponseData> LogInUser(string email, string password, UserRole userRole)
        {
            try
            {
                return _accountService.LogIn(email, password, userRole)
                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulLogIn, email))
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureLogIn, email));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionLogIn, email));
            }
        }

        public ActionResult<ResponseData> RegisterUser(User user)
        {
            try
            {
                return _accountService.Register(user)
                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulRegistered, user.Email))
                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureRegister, user.Email));
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionRegister, user.Email));
            }
        }

//        public ActionResult<ResponseData> RegisterLecturer(User registerUser)
//        {
//            try
//            {
//                return _accountService.Register(email, password, userRole)
//                    ? ResponseData.SendSuccessMsg(string.Format(DefaultConstants.SuccessfulRegistered, email))
//                    : ResponseData.SendFailMsg(string.Format(DefaultConstants.FailureRegister, email));
//            }
//            catch (Exception e)
//            {
//                return ResponseData.SendFailMsg(string.Format(DefaultConstants.ExceptionRegister, email));
//            }
//        }
    }
}