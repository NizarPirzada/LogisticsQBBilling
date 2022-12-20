using FTCommon.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FTCommon.Email
{
    public class EmailService : IEmailService
    {
        private readonly IHostEnvironment _env;

        public EmailService(IHostEnvironment env)
        {
            _env = env;
        }
        public async Task SendAccountEmails(string url, string email, string subject, string templatePath)
        {
            string body = FormatEmailBody(new List<KeyValuePair<string, string>> {
                 new KeyValuePair<string, string>("{verfication_code}",url),
                 new KeyValuePair<string, string>("{useremail}",email)
            }, templatePath);
            await SendEmailUsingSendGrid(email, subject, body);
            
        }
        public async Task SendVerificationCode(string code, string email, string subject, string templatePath)
        {
            string body = FormatEmailBody(new List<KeyValuePair<string, string>> {
                 new KeyValuePair<string, string>("{verfication_code}",code),
                 new KeyValuePair<string, string>("{useremail}",email)
            }, templatePath);
            await SendEmailUsingSendGrid(email, subject, body);
        }
        public async Task SendScenarioCompareLink(string  url ,string email, string subject, string templatePath)
        {
            string body = FormatEmailBody(new List<KeyValuePair<string, string>> {
                 new KeyValuePair<string, string>("{click_url}",url)
            }, templatePath);
            await SendEmailUsingSMTP(email, subject, body);
        }
        private string FormatEmailBody(IEnumerable<KeyValuePair<string, string>> tokens, string emailTemplatePath)
        {
            string _body = string.Empty;
            var builder = new BodyBuilder();
            var pathToFile = _env.ContentRootPath
                        + Path.DirectorySeparatorChar.ToString()
                        + emailTemplatePath;
            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }
            _body = builder.HtmlBody;
            if (tokens != null)
            {
                foreach (var item in tokens)
                {
                    _body = _body.Replace(item.Key, item.Value);
                }
            }
            return _body;
        }
        public async Task SendEmailUsingSendGrid(string Email, string subject, string body)
        {
            try
            {
                var client = new SendGridClient(UtilityHelper._config["Sendgrid:ApiKey"]);
                var mailContent = new SendGridMessage()
                {
                    From = new EmailAddress(UtilityHelper._config["Sendgrid:EmailFrom"], "Compliance Studio"),
                    Subject = subject,
                    PlainTextContent = body,
                    HtmlContent = body
                };
                mailContent.AddTo(new EmailAddress(Email));
                var response = await client.SendEmailAsync(mailContent);
            }
            catch (Exception ex)
            {

            }
        }
        public async Task SendEmailUsingSMTP(string Email, string subject, string body)
        {
            var smtp = new SmtpClient
            {
                Host = UtilityHelper._config["EmailConfigurations-Gmail:Host"],
                Port = Convert.ToInt32(UtilityHelper._config["EmailConfigurations-Gmail:Port"]),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(UtilityHelper._config["EmailConfigurations-Gmail:Email"], UtilityHelper._config["EmailConfigurations-Gmail:Password"]),
                Timeout = 10000
            };
            var message = new MailMessage(UtilityHelper._config["EmailConfigurations-Gmail:Email"], Email)
            {
                IsBodyHtml = true,
                Subject = subject,
                Body = body,
            };
            try
            {
                await smtp.SendMailAsync(message);

            }
            catch (Exception ex)
            {

            }
        }
    }
}
