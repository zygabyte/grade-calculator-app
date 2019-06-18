namespace GradeCalculatorApp.Core.Messaging
{
    public class SmtpCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string MailFrom { get; set; }
        public string MailHead { get; set; }
        public string Server { get; set; }
        public int ServerPort { get; set; }
        public bool SslMode { get; set; }
        public string MailTemplate { get; set; }
    }
}