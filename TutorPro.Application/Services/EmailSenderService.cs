using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Hosting;
using Umbraco.Cms.Core.Configuration.Models;
using SecureSocketOptions = MailKit.Security.SecureSocketOptions;

namespace TutorPro.Application.Services
{
    using Interfaces;
	using Microsoft.Extensions.Logging;
	using Models;
    using System.Globalization;
    using System.Text;

    public class EmailSenderService(
        IOptions<GlobalSettings> config,
        IHostEnvironment environment,
        ILogger<EmailSenderService> logger) : IEmailSenderService      
    {
        public async Task SendEmailAsync(FormRequestDTO formRequest, string subject, CancellationToken cancellation = default, bool isHtml = true)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(config.Value.Smtp.Username, config.Value.Smtp.From ));

            message.To.Add(new MailboxAddress(config.Value.Smtp.Username, config.Value.Smtp.From ));
            //Go throught emails list at umbraco and add to message 
            if(formRequest.AdditionalEmail != null)
            {
                foreach (var email in formRequest.AdditionalEmail)
                {
                    message.To.Add(new MailboxAddress(config.Value.Smtp.Username, email));
                }
            }
            message.Subject = subject;
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = await GenerateTemplateAsync(formRequest);
            message.Body = bodyBuilder.ToMessageBody();

			await SendMessageAsync(message, cancellation);
        }

		public async Task SendBlogArticleEmailAsync(string email, NewBlogView blogArticle, string subject, string culture, CancellationToken cancellation = default, bool isHtml = true)
		{
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress(config.Value.Smtp.Username, config.Value.Smtp.From));

			message.To.Add(new MailboxAddress(email, email));
			message.Subject = subject;

			var bodyBuilder = new BodyBuilder();

			bodyBuilder.HtmlBody = await GenerateBlogArticleTemplateAsync(blogArticle, culture, email);

			var logoFilePath = $"wwwroot/images/logo-2.png";
			var blogImageFilePath = $"wwwroot{blogArticle.ImageUrl}";

			if (File.Exists(logoFilePath))
			{
				var logo = new MimePart("image", "png")
				{
					Content = new MimeContent(File.OpenRead(logoFilePath)),
					ContentDisposition = new ContentDisposition(ContentDisposition.Inline),
					ContentTransferEncoding = ContentEncoding.Base64,
					FileName = "logo.png",
					ContentId = "logo"
				};

				bodyBuilder.LinkedResources.Add(logo);
			}
			else
			{
                logger.LogWarning($"{logoFilePath} - no image on this path");
			}

			if (File.Exists(blogImageFilePath))
			{
				var blogImage = new MimePart("image", "png")
				{
					Content = new MimeContent(File.OpenRead(blogImageFilePath)),
					ContentDisposition = new ContentDisposition(ContentDisposition.Inline),
					ContentTransferEncoding = ContentEncoding.Base64,
					FileName = "blog.png",
					ContentId = "blog"
				};

				bodyBuilder.LinkedResources.Add(blogImage);
			}
			else
			{
				logger.LogWarning($"{blogImageFilePath} - no image on this path");
			}			

			message.Body = bodyBuilder.ToMessageBody();

			await SendMessageAsync(message, cancellation);
		}

		private async Task<string> GenerateBlogArticleTemplateAsync(NewBlogView blogArticle, string culture, string email, string templateFileName = Constants.EmailTemplates.BlogArticleTemplateEmail, CancellationToken cancellation = default)
        {
            var path = environment.ContentRootPath;
            var body = await File.ReadAllTextAsync(Path.Combine(path, templateFileName + $"/{culture}.html"), cancellation);
            StringBuilder stringBuilder = new StringBuilder(body);
            stringBuilder
                .Replace("{Title}", blogArticle.Title)
                .Replace("{Url}", blogArticle.Url)
                .Replace("{DateTime}", blogArticle.DateTime.ToString("D", new CultureInfo(culture)))
                .Replace("{UnsubscribeUrl}", blogArticle.UnsubscribeUrl + "?email=" + $"{email}|{culture}")
                .Replace("{SiteUrl}", blogArticle.SiteUrl);
            body = stringBuilder.ToString();

            return body;
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

		private async Task SendMessageAsync(MimeMessage message, CancellationToken cancellation)
		{
			using var client = new SmtpClient();

			await client.ConnectAsync(config.Value.Smtp.Host, config.Value.Smtp.Port, SecureSocketOptions.StartTls, cancellation);
			await client.AuthenticateAsync(config.Value.Smtp.Username, config.Value.Smtp.Password, cancellation);

			await client.SendAsync(message, cancellation);
			await client.DisconnectAsync(true, cancellation);
		}
	}
}
