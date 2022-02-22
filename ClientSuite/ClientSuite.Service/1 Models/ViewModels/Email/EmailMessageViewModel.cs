using System;

namespace ClientSuite.Service
{
    public class EmailMessageViewModel
    {
        public int Id { get; set; }

        public int FromUserId { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime SentDate { get; set; }

        public string AttachmentJson { get; set; }


    }
}

