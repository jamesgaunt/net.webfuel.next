using System;
using System.Collections.Generic;

namespace Webfuel
{
    public class EmailRelayData
    {
        public string SystemName { get; set; } = String.Empty;
        public string AccountName { get; set; } = String.Empty;
        public string SendTo { get; set; } = String.Empty;
        public string SendCc { get; set; } = String.Empty;
        public string SendBcc { get; set; } = String.Empty;
        public string SentBy { get; set; } = String.Empty;
        public string ReplyTo { get; set; } = String.Empty;
        public string Subject { get; set; } = String.Empty;
        public string HtmlBody { get; set; } = String.Empty;
        public List<string> Attachments { get; } = new List<string>();

        public bool IsValid()
        {
            if (String.IsNullOrEmpty(SystemName))
                return false;
            if (String.IsNullOrEmpty(AccountName))
                return false;
            if (String.IsNullOrEmpty(ReplyTo))
                return false;
            if (String.IsNullOrEmpty(SendTo) && String.IsNullOrEmpty(SendCc) && String.IsNullOrEmpty(SendBcc))
                return false;

            return true;
        }
    }
}
