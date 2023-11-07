using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Common;

namespace Webfuel
{
    public interface IEmailService
    {
        Task SendAsync(
            string sendTo,
            string sendCc,
            string sendBcc,
            string sentBy,
            string replyTo,
            string subject,
            string htmlBody);
    }

    [Service(typeof(IEmailService))]
    internal class EmailService : IEmailService
    {
        private readonly IEmailRelayService _emailRelayService;
        private readonly IEmailLogRepository _emailLogRepository;

        public EmailService(IEmailRelayService emailRelayService, IEmailLogRepository emailLogRepository)
        {
            _emailRelayService = emailRelayService;
            _emailLogRepository = emailLogRepository;   
        }

        public async Task SendAsync(
            string sendTo,
            string sendCc,
            string sendBcc,
            string sentBy,
            string replyTo,
            string subject,
            string htmlBody)
        {
            await _emailRelayService.SendAsync(
                accountName: "rss-ucl",
                sendTo: sendTo,
                sendCc: sendCc,
                sendBcc: sendBcc,
                replyTo: replyTo,
                sentBy: sentBy,
                subject: subject,
                htmlBody: htmlBody,
                attachments: null);

            await _emailLogRepository.InsertEmailLog(new EmailLog
            {
                SendTo = sendTo,
                SendCc = sendCc,
                SendBcc = sendBcc,
                ReplyTo = replyTo,
                SentBy = sentBy,
                Subject = subject,
                HtmlBody = htmlBody,
                SentAt = DateTimeOffset.UtcNow,
            });
        }
    }
}