using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayStationClub.Infrastructure
{
    public class SendGridEmailSender : IEmailSender
    {
        private readonly ILogger _logger;

        public SendGridEmailSenderOptions Options { get; }

        public SendGridEmailSender(IOptions<SendGridEmailSenderOptions> options, ILogger<SendGridEmailSender> logger)
        {
            Options = options.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            if (string.IsNullOrEmpty(Options.ApiKey)) throw new Exception("Null SendGridKey");
            await Execute(Options.ApiKey, subject, htmlMessage, email);
        }

        private async Task Execute(string apiKey, string subject, string htmlMessage, string email)
        {
            var client = new SendGridClient(apiKey);
            var message = new SendGridMessage()
            {
                From = new EmailAddress(Options.SenderEmail, Options.SenderName),
                Subject = subject,
                PlainTextContent = htmlMessage,
                HtmlContent = htmlMessage
            };
            message.AddTo(new EmailAddress(email));

            message.SetClickTracking(false, false);
            var response = await client.SendEmailAsync(message);
            _logger.LogInformation(response.IsSuccessStatusCode ? $"Email to {email} queued successfully!" 
                                                                : $"Failure email to {email}");
        }
    }
}
