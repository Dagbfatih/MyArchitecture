using Core.Aspects.Autofac.Performance;
using Core.Utilities.Messages;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Services.Translate;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Utilities.Mail
{
    public class EmailManager : CoreMessagesService, IEmailService
    {
        private IEmailConfiguration _emailConfiguration;
        private readonly IConfiguration _configuration;

        public EmailManager(IEmailConfiguration emailConfiguration, IConfiguration configuration)
        {
            _emailConfiguration = emailConfiguration;
            _configuration = configuration;
        }

        [PerformanceAspect(5)]
        public IResult Send(EmailMessage emailMessage)
        {
            var message = new MimeMessage();
            message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
            message.From.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

            message.Subject = emailMessage.Subject;
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = emailMessage.Content
            };

            using (var emailClient = new SmtpClient())
            {
                _emailConfiguration.SmtpPort = Convert.ToInt32(_configuration.GetSection("EmailConfiguration").GetSection("SmtpPort").Value);
                _emailConfiguration.SmtpServer = _configuration.GetSection("EmailConfiguration").GetSection("SmtpServer").Value;
                _emailConfiguration.SmtpUsername = _configuration.GetSection("EmailConfiguration").GetSection("SmtpUsername").Value;
                _emailConfiguration.SmtpPassword = _configuration.GetSection("EmailConfiguration").GetSection("SmtpPassword").Value;

                //emailClient.Connect(
                //    _configuration.GetSection("EmailConfiguration").GetSection("SmtpServer").Value,
                //    Convert.ToInt32(_configuration.GetSection("EmailConfiguration").GetSection("SmtpPort").Value),
                //    MailKit.Security.SecureSocketOptions.Auto);
                //emailClient.Send(message);
                //emailClient.Disconnect(true);

                emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);

                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);

                emailClient.Send(message);

                emailClient.Disconnect(true);

                return new SuccessResult(_coreMessages.EmailSent);
            }
        }

        public IDataResult<List<EmailMessage>> ReceiveEmail(int maxCount = 10)
        {
            using (var emailClient = new Pop3Client())
            {
                emailClient.Connect(_emailConfiguration.PopServer, _emailConfiguration.PopPort, true);

                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                emailClient.Authenticate(_emailConfiguration.PopUsername, _emailConfiguration.PopPassword);

                List<EmailMessage> emails = new List<EmailMessage>();
                for (int i = 0; i < emailClient.Count && i < maxCount; i++)
                {
                    var message = emailClient.GetMessage(i);
                    var emailMessage = new EmailMessage
                    {
                        Content = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody,
                        Subject = message.Subject
                    };
                    emailMessage.ToAddresses.AddRange(message.To.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
                    emailMessage.FromAddresses.AddRange(message.From.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
                    emails.Add(emailMessage);
                }

                return new SuccessDataResult<List<EmailMessage>>(emails);
            }
        }
    }
}
