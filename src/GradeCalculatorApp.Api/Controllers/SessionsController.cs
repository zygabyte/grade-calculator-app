using System;
using GradeCalculatorApp.Data.Domains;
using GradeCalculatorApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace GradeCalculatorApp.Api.Controllers
{
    public class SessionsController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }

        public ActionResult<ResponseData> CreateSession(Session session)
        {
            try
            {
                
                return ResponseData.SendSuccessMsg();
            }
            catch (Exception e)
            {
                return ResponseData.SendFailMsg();
            }
        }
    }
}