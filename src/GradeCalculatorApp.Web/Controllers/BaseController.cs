using System;
using GradeCalculatorApp.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GradeCalculatorApp.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly IAccountService _accountService;
        
        public BaseController(IAccountService accountService) => _accountService = accountService;
        
//        protected override void Initialize(RequestContext requestContext)
//        {
//            base.Initialize(requestContext);
//            Response.
//            Response.Cache.SetCacheability(HttpCacheability.NoCache);
//            Response.Cache.SetNoStore();
//        }
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //return;
            try
            {
                if (_accountService.IsUserSessionActive()) return;

                filterContext.Result = RedirectToAction("LogIn", "Account");
                _accountService.ClearSession();
            }
            catch (Exception e)
            {
                filterContext.Result = RedirectToAction("LogIn", "Account");
            }
        }
    }
}