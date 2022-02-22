using System;

namespace ClientSuite.Service
{
    public class ContactsViewModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public int? AddedBy { get; set; }

        public DateTime? DateAdded { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? DateModified { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string CategoryId { get; set; }


    }
}

