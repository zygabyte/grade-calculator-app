using System;
using System.Threading;
using GradeCalculatorApp.Core.Constants;
using GradeCalculatorApp.Core.Messaging;
using GradeCalculatorApp.Core.Services.Interfaces;
using GradeCalculatorApp.Data.Models;
using Microsoft.Extensions.Options;

namespace GradeCalculatorApp.Core.Services.Implementations
{
    public class MailService : IMailService
    {
        private readonly IMessagingService _messagingService;
        private readonly MailConstants _mailConstants;
        
        public MailService(IMessagingService messagingService, IOptions<HostName> options)
        {
            _messagingService = messagingService;
            _mailConstants = new MailConstants(options);
        }
        
        public void SendRegisterMail(string toEmail, string fullName, string userRole, string tokenMap)
        {
            try
            {
                SendMail(toEmail, _mailConstants.RegisterHeading, string.Format(_mailConstants.RegisterBody, fullName, userRole, _mailConstants.BaseRegisterUrl(), tokenMap, toEmail));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        private void SendMail(string toEmail, string mailHeading, string mailBody, string ccEmail = "", string bccEmail = "", string attachment = "")
        {
            new Thread(() =>
            {
                try
                {
                    _messagingService.SendMail(toEmail, ccEmail, bccEmail, mailHeading, mailBody, attachment);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }).Start();
        }
    }
}