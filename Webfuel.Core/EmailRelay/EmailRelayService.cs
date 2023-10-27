using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public interface IEmailRelayService
    {
        Task SendAsync(
            string accountName,
            string sendTo,
            string sendCc,
            string sendBcc,
            string sentBy,
            string replyTo,
            string subject,
            string htmlBody,
            IEnumerable<EmailRelayAttachment>? attachments);
    }

    [Service(typeof(IEmailRelayService))]
    internal class EmailRelayService : IEmailRelayService
    {
        private readonly IEmailRelayConfiguration EmailRelayConfiguration;

        public EmailRelayService(IEmailRelayConfiguration emailRelayConfiguration)
        {
            EmailRelayConfiguration = emailRelayConfiguration;
        }

        public async Task SendAsync(
            string accountName,
            string sendTo,
            string sendCc,
            string sendBcc,
            string sentBy,
            string replyTo,
            string subject,
            string htmlBody,
            IEnumerable<EmailRelayAttachment>? attachments)
        {
            try
            {
                var data = new EmailRelayData
                {
                    SystemName = EmailRelayConfiguration.SystemName,
                    AccountName = accountName,
                    SendTo = sendTo,
                    SendCc = sendCc,
                    SendBcc = sendBcc,
                    SentBy = sentBy,
                    ReplyTo = replyTo,
                    Subject = subject,
                    HtmlBody = htmlBody,
                };

                await UploadAttachmentsAsync(data, attachments);

                await Queue.SendMessageAsync(SafeJsonSerializer.Serialize(data));
            }
            catch { /* GULP */ }
        }

        async Task UploadAttachmentsAsync(EmailRelayData data, IEnumerable<EmailRelayAttachment>? attachments)
        {
            if (attachments == null || attachments.Count() == 0)
                return;

            var root = GenerateAttachmentRoot();

            foreach(var attachment in attachments)
            {
                try
                {
                    if (attachment.Stream == null || attachment.Stream.Length == 0)
                        continue; // Nothing to attach so ignore

                    attachment.Stream.Position = 0;

                    var path = root + attachment.FileName;

                    await Container.UploadBlobAsync(path, attachment.Stream);
                    
                    data.Attachments.Add(path);

                }
                catch (Exception ex) 
                {
                    data.Attachments.Add("EXCEPTION: " + ex.Message);
                }
            }
        }

        string GenerateAttachmentRoot()
        {
            var d = DateTime.Today;
            return $"/{DateTime.Today.ToString("yyyy-MM-dd")}/{Guid.NewGuid().ToString()}/";
        }

        QueueStorage Queue
        {
            get
            {
                if (_queue == null)
                {
                    _queue = new QueueStorage(EmailRelayConfiguration.StorageConnectionString, EmailRelayConfiguration.QueueName);
                }
                return _queue;
            }
        }
        QueueStorage? _queue = null;

        BlobStorage Container
        {
            get
            {
                if(_container == null)
                {
                    _container = new BlobStorage(EmailRelayConfiguration.StorageConnectionString, EmailRelayConfiguration.ContainerName);
                }
                return _container;
            }
        }
        BlobStorage? _container = null;
    }
}