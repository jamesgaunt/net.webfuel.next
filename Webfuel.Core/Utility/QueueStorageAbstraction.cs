using Azure.Storage.Queues;
using System.Text;

namespace Webfuel
{
    public class QueueMessage
    {
        private readonly QueueClient Client;
        private readonly Azure.Storage.Queues.Models.QueueMessage Message;

        public QueueMessage(QueueClient client, Azure.Storage.Queues.Models.QueueMessage message)
        {
            Client = client;
            Message = message;
            DecodeBody();
        }

        public string MessageId { get { return Message.MessageId; } }

        public string Body { get; private set; } = String.Empty;

        public async Task DeleteAsync()
        {
            await Client.DeleteMessageAsync(Message.MessageId, Message.PopReceipt);
        }

        void DecodeBody()
        {
            var body = Message.Body.ToString();
            try
            {
                var bytes = Convert.FromBase64String(body);
                body = Encoding.UTF8.GetString(bytes);
            }
            catch { /* gulp */ }
            Body = body;
        }
    }

    public class QueueStorage
    {
        private readonly QueueClient Client;

        public QueueStorage(string connectionString, string queueName)
        {
            Client = new QueueClient(connectionString, queueName);
        }

        public async Task SendMessageAsync(string message)
        {
            await Client.SendMessageAsync(new BinaryData(message));
        }

        public async Task<List<QueueMessage>> ReceiveMessagesAsync(int maxMessages = 10)
        {
            var response = new List<QueueMessage>();
            foreach (var message in (await Client.ReceiveMessagesAsync(maxMessages: 10)).Value)
            {
                if (message == null)
                    continue;
                response.Add(new QueueMessage(Client, message));
            }
            return response;
        }
    }
}
