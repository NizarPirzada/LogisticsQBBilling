using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTCommon.Email
{
    public interface IEmailService
    {
        Task SendAccountEmails(string url, string email, string subject, string templatePath);
        Task SendVerificationCode(string code, string email, string subject, string templatePath);
        Task SendEmailUsingSMTP(string Email, string subject, string body);
        Task SendEmailUsingSendGrid(string Email, string subject, string body);
        Task SendScenarioCompareLink(string url, string email, string subject, string templatePath);
    }
}
