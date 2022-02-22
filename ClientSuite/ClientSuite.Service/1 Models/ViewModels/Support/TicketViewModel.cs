using System;

namespace ClientSuite.Service
{
    public class TicketViewModel
    {
        public int Id { get; set; }

        public string TicketSubject { get; set; }

        public string TicketBody { get; set; }

        public int AddedBy { get; set; }

        public DateTime DateAdded { get; set; }

        public int? ModifiedBy { get; set; }

        public string Attachment { get; set; }

        public bool IsClose { get; set; }

        public DateTime? DateModified { get; set; }

        public bool IsKnowledgeBase { get; set; }

        public string ProductId { get; set; }

        public string ParentId { get; set; }

        public string PriorityId { get; set; }


    }
}

