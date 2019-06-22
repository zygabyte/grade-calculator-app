using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
//using SendGrid;
//using SendGrid.Helpers.Mail;

namespace GradeCalculatorApp.Core.Messaging
{
    public interface IMessagingService
    {
        bool SendMail(string destination, string ccDestination, string bccDestination,
            string messageHeading, string message, string attachments);
    }

    public class MessagingService : IMessagingService
    {
        private readonly SmtpCredentials _smtpCredentials;
        public MessagingService(IOptions<SmtpCredentials> smtpCredentials)
        {
            _smtpCredentials = smtpCredentials.Value;
        }
        public bool SendMail(string destination, string ccDestination, string bccDestination,
              string messageHeading, string message, string attachments)
        {
            try
            {
//                var mailSettings = Setting.MailSettings();

//                if (string.IsNullOrEmpty(bccDestination))
//                    bccDestination = mailSettings.SmtpAuditMailBox;

                var destinations = destination.Split(';');
                var sendFrom = new MailAddress(_smtpCredentials.MailFrom, _smtpCredentials.MailHead);

                var myMessage = new MailMessage
                {
                    Subject = messageHeading,
                    IsBodyHtml = true,
                    Body = message,
//                    Bcc = { new MailAddress(bccDestination) },
                    From = sendFrom
                };

                foreach (var currentDestination in destinations)
                {
                    if (string.IsNullOrEmpty(currentDestination))
                        continue;

                    myMessage.To.Add(currentDestination);
                }

                if (!string.IsNullOrEmpty(ccDestination))
                    foreach (var currentCcDestination in ccDestination.Split(';'))
                    {
                        if (string.IsNullOrEmpty(currentCcDestination))
                            continue;

                        myMessage.CC.Add(currentCcDestination);
                    }

                if (!string.IsNullOrEmpty(attachments))
                {
                    var attachmentsList = attachments.Split(';').ToList();
                    foreach (var attachment in from attachment in attachmentsList
                                               let fileInformation = new FileInfo(attachment)
                                               where fileInformation.Exists
                                               select attachment)
                    {
                        myMessage.Attachments.Add(new Attachment(attachment));
                    }
                }

                string template;

                
                try
                {
                    var templateHtml = _smtpCredentials.MailTemplate;
                    
                    using (var webClient = new WebClient())
                    {
                        //A web location for the template of the email body
                        template = webClient.DownloadString(templateHtml);
                    }

//                    if (templateHtml == null)
//                    {
//                        var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Views", "Security", "MailTemplate.cshtml") ;
//                        if (File.Exists(dir))
//                            template = File.ReadAllText(dir);
//                    }
//                    else
                    {
                        
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }

                template = template.Replace("{title}", messageHeading).Replace("{message}", message);

                var htmlView = AlternateView.CreateAlternateViewFromString(template, null, "text/html");
//                var htmlView = AlternateView.CreateAlternateViewFromString("<h1>Hello world</h1>", null, "text/html");
                myMessage.AlternateViews.Add(htmlView);

                var smClient = new SmtpClient(_smtpCredentials.Server)
                {
                    Credentials =
                        new NetworkCredential(_smtpCredentials.Username, _smtpCredentials.Password),
                    EnableSsl = _smtpCredentials.SslMode,
                    Host = _smtpCredentials.Server,
                    Port = _smtpCredentials.ServerPort,
                    Timeout = 200000
                };

                smClient.Send(myMessage);
                
                myMessage.Dispose();
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return false;
            }
        }
        
//        public bool SendMail(string destination, string ccDestination, string bccDestination,
//              string messageHeading, string message, string attachments)
//        {
//            try
//            {
//                string template;
//
//                
//                try
//                {
//                    var templateHtml = _smtpCredentials.MailTemplate;
//                    
//                    using (var webClient = new WebClient())
//                    {
//                        //A web location for the template of the email body
//                        template = webClient.DownloadString(templateHtml);
//                    }
//
////                    if (templateHtml == null)
////                    {
////                        var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Views", "Security", "MailTemplate.cshtml") ;
////                        if (File.Exists(dir))
////                            template = File.ReadAllText(dir);
////                    }
////                    else
//                    {
//                        
//                    }
//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine(e);
//                    return false;
//                }
//
//                template = template.Replace("{title}", messageHeading).Replace("{message}", message);
////                var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
//                var client = new SendGridClient(_smtpCredentials.Password);
//                var from = new EmailAddress(_smtpCredentials.MailFrom, "Example User");
//                var subject = _smtpCredentials.MailHead;
//                var to = new EmailAddress(destination, "Example User");
////                var plainTextContent = "and easy to do anywhere, even with C#";
//                var htmlContent = template;
//                var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
//                var response = client.SendEmailAsync(msg).Result;
//
//                return response.StatusCode == HttpStatusCode.Accepted;
//            }
//            catch (Exception exception)
//            {
//                Console.WriteLine(exception);
//                return false;
//            }
//        }
    }
}