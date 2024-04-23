using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Hosting;
using Umbraco.Cms.Core.Configuration.Models;
using SecureSocketOptions = MailKit.Security.SecureSocketOptions;

namespace TutorPro.Application.Services
{
    using Interfaces;
    using Models;
    using System.Text;

    public class EmailSenderService(
        IOptions<GlobalSettings> config,
        IHostEnvironment environment) : IEmailSenderService
    {
        public async Task SendEmailAsync(FormRequestDTO formRequest, string subject, CancellationToken cancellation = default, bool isHtml = true)
        {
            var message = new MimeMessage();
            message.To.Add(new MailboxAddress(config.Value.Smtp.From, config.Value.Smtp.Username));
            message.Subject = subject;
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = await GenerateTemplateAsync(formRequest);
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();

            await client.ConnectAsync(config.Value.Smtp.Host, config.Value.Smtp.Port, SecureSocketOptions.StartTls, cancellation);
            await client.AuthenticateAsync(config.Value.Smtp.Username, config.Value.Smtp.Password, cancellation);

            await client.SendAsync(message, cancellation);
            await client.DisconnectAsync(true, cancellation);
        }

        private async Task<string> GenerateTemplateAsync(FormRequestDTO formRequest, string templateFileName = Constants.EmailTemplates.RequestTemplateEmail, CancellationToken cancellation = default)
        {
            var path = environment.ContentRootPath;
            var body = await File.ReadAllTextAsync(Path.Combine(path, templateFileName), cancellation);
            StringBuilder stringBuilder = new StringBuilder(body);
            stringBuilder
                .Replace(Constants.EmailProperty.SenderName, formRequest.SenderName)
                .Replace(Constants.EmailProperty.SenderEmail, formRequest.SenderEmail)
                .Replace(Constants.EmailProperty.SenderPhone, formRequest.SenderPhone)
                .Replace(Constants.EmailProperty.SenderMessage, formRequest.SenderMessage);
            body = stringBuilder.ToString();
            return body;
        }
    }
}
