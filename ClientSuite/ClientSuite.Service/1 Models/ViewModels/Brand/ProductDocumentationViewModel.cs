using System;

namespace ClientSuite.Service
{
    public class ProductDocumentationViewModel
    {
        public int Id { get; set; }

        public string Details { get; set; }

        public int? AddedBy { get; set; }

        public DateTime? DateAdded { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? DateModified { get; set; }

        public string Title { get; set; }

        public string ProductId { get; set; }

        public string ParentId { get; set; }


    }
}

