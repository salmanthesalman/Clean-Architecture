using Application.Features.Infrastructure;
using Application.Features.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Infrastructure.Mail
{
    public class EmailService : IEmailServices
    {
        public EmailSetting _emailSetting { get; set; }
        public ILogger<EmailSetting> _logger { get; set; }
        public EmailService(IOptions<EmailSetting> emailSetting, ILogger<EmailSetting> logger)
        {
            _emailSetting = emailSetting.Value;
            _logger = logger;
        }
        public async Task<bool> SendEmail(Email email)
        {
            var client = new SendGridClient("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");

            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody = email.Body;

            var from = new EmailAddress
            {
                Email = _emailSetting.FromAddress,
                Name = _emailSetting.FromName
            };

            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var response = await client.SendEmailAsync(sendGridMessage);

            _logger.LogInformation("Email sent.");

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            _logger.LogError("Email sending failed.");

            return false;
        }
    }
}
