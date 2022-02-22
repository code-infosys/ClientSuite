using System;

namespace ClientSuite.Service
{
    public class TransactionViewModel
    {
        public int Id { get; set; }

        public string Source { get; set; }

        public decimal Amount { get; set; }

        public string TransactionNumber { get; set; }

        public int? AddedBy { get; set; }

        public DateTime? DateAdded { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? DateModified { get; set; }

        public string ProductId { get; set; }


    }
}

