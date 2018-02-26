using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QhTemplate.ApplicationService.Utils.EmailUtils
{
    public class EmailHelper
    {
        private readonly string _configName;

        public EmailHelper() : this("emailconfig.json")
        {
        }

        public EmailHelper(string configName)
        {
            _configName = configName;
        }


        public async Task SendEmailAsync(EmailRecevierConfig emailBody)
        {
            var setting = await GetConfigAsync();
            using (var mail = new MailMessage())
            {
                mail.From = new MailAddress(setting.From, setting.SenderName);
                mail.To.Add(new MailAddress(emailBody.EmailAdd, emailBody.Addressee));
                mail.Subject = emailBody.Subject;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Body = emailBody.Content;
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;
                if (!string.IsNullOrWhiteSpace(emailBody.FilePath))
                {
                    if (!File.Exists(emailBody.FilePath))
                    {
                        throw new FileNotFoundException($"{_configName}文件未找到");
                    }

                    mail.Attachments.Add(new Attachment(emailBody.FilePath));
                }

                using (var smtp = new SmtpClient())
                {
                    smtp.Host = setting.SmtpServer;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential(setting.From, setting.Password);
                    await smtp.SendMailAsync(mail);
                }
            }
        }

        private async Task<EmailSenderConfig> GetConfigAsync()
        {
            if (!File.Exists(_configName))
            {
                throw new FileNotFoundException($"{_configName}文件未找到");
            }

            using (var stream = new StreamReader("emailconfig.json"))
            {
                var json = await stream.ReadToEndAsync();
                return JsonConvert.DeserializeObject<EmailSenderConfig>(json);
            }
        }
    }
}