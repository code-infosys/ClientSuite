using System;

namespace ClientSuite.Service
{
    public class ClientCategoryViewModel
    {
        public int Id { get; set; }

        public int? AddedBy { get; set; }

        public DateTime? DateAdded { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? DateModified { get; set; }

        public string UserId { get; set; }

        public string CategoryId { get; set; }


    }
}

