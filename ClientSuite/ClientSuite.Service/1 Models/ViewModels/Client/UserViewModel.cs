using System;

namespace ClientSuite.Service
{
    public class UserViewModel
    {
        public DateTime? DateAdded { get; set; }

        public DateTime? DateModified { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public bool IsActive { get; set; }

        public string ChangePasswordCode { get; set; }

        public string Otp { get; set; }

        public bool IsConfirm { get; set; }

        public string ProfilePicture { get; set; }

        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string CompanyName { get; set; }

        public string CompanyPhone { get; set; }

        public string CompanyMobile { get; set; }

        public string CompanyAddress { get; set; }

        public string RoleId { get; set; }

        public string CurrencyId { get; set; }

        public string CountryId { get; set; }


    }
}

