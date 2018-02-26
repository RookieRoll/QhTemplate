namespace QhTemplate.ApplicationService.Utils.EmailUtils
{
    internal class EmailSenderConfig
    {
        public string From { get; set; }
        public string Password { get; set; }
        public string SenderName { get; set; }
        public string SmtpServer { get; set; }
    }

    public class EmailRecevierConfig
    {
        public string Content { get; set; }
        public string Subject { get; set; }
        public string FilePath { get; set; }
        public string EmailAdd { get; set; }
        public string Addressee { get; set; }
    }
}