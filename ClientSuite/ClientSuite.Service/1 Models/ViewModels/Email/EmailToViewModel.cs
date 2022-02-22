using System;

namespace ClientSuite.Service
{
    public class EmailToViewModel
    {
        public int Id { get; set; }

        public int EmailMessageId { get; set; }

        public int ToUserId { get; set; }

        public DateTime? DateCreated { get; set; }


    }
}

