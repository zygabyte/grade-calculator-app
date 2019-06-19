using System;
using GradeCalculatorApp.Data.Models;
using Microsoft.Extensions.Options;

namespace GradeCalculatorApp.Core.Constants
{
    public class MailConstants
    {
        private readonly HostName _hostName;
        public MailConstants(IOptions<HostName> options) => _hostName = options.Value;

        private string BaseUrl () => _hostName.BaseUrl;
        public string BaseRegisterUrl () => new Uri(new Uri(BaseUrl()), "/Account/Register").ToString();

        public string RegisterHeading { get; } = "Complete Registration";
        public string RegisterBody { get; } = "Hello {0}! <br />You have successfully been profiled into the Grade Calculator System as a {1} <br />Kindly click <a href={2}/{3}>here</a> to complete your registration. <br><br>If the link does not work, kindly copy and paste this link onto your browser - <span style=\"font-weight: bold;\">{2}/{3}</span> <br><br>PS - Your email ({4}) will be your username, while you will be requested to set a new password";
    }
}