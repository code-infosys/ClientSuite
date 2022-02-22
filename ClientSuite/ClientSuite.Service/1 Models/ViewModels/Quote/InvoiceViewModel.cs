using System;

namespace ClientSuite.Service
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string BusinessLogo { get; set; }

        public string BusinessDetails { get; set; }

        public string BillTo { get; set; }

        public string ClientDetails { get; set; }

        public string Currency { get; set; }

        public DateTime Dated { get; set; }

        public DateTime? DueDate { get; set; }

        public string ItemJson { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Tax { get; set; }

        public decimal TotalDue { get; set; }

        public decimal? PaidAmount { get; set; }

        public decimal? BalanceDue { get; set; }

        public bool IsPaid { get; set; }

        public int? AddedBy { get; set; }

        public DateTime? DateAdded { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? DateModified { get; set; }

        public bool IsQuote { get; set; }

        public string Note { get; set; }

        public string TermsAndCondition { get; set; }

        public string Sign { get; set; }

        public string UserId { get; set; }


    }
}

