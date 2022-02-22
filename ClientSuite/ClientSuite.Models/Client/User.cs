using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace ClientSuite.Models
{
    public class User : BaseEntity
    { 
       [DisplayName("Date Added")]
       [Column(TypeName = "datetime")]
       public DateTime? DateAdded { get; set; }

       [DisplayName("Date Modified")]
       [Column(TypeName = "datetime")]
       public DateTime? DateModified { get; set; }

       [DisplayName("Full Name")]
       [StringLength(300)] 
       public string FullName { get; set; }

       [DisplayName("Email")]
       [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
       [StringLength(50)] 

       public string Email { get; set; }

       [Required]
       [DisplayName("Mobile Number")]
       [StringLength(30)] 
       public string MobileNumber { get; set; }

       [Required]
       [DisplayName("Is Active")]
       public bool IsActive { get; set; }

       [DisplayName("Change Password Code")]
       [StringLength(100)] 
       public string ChangePasswordCode { get; set; }

       [DisplayName("Otp")]
       [StringLength(20)] 
       public string Otp { get; set; }

       [Required]
       [DisplayName("Is Confirm")]
       public bool IsConfirm { get; set; }

       [DisplayName("Profile Picture")]
       [StringLength(100)] 
       public string ProfilePicture { get; set; }

       [Required]
       [DisplayName("User Name")]
       [StringLength(100)] 
       public string UserName { get; set; }

       [Required]
       [DisplayName("Password")]
       [StringLength(500)] 
       public string Password { get; set; }

       [Required]
       [DisplayName("Company Name")]
       [StringLength(200)] 
       public string CompanyName { get; set; }

       [DisplayName("Company Phone")]
       [StringLength(20)] 
       public string CompanyPhone { get; set; }

       [DisplayName("Company Mobile")]
       [StringLength(20)] 
       public string CompanyMobile { get; set; }

       [DisplayName("Company Address")]
       [StringLength(1000)] 
       public string CompanyAddress { get; set; }

       [DisplayName("Role")]
       public int? RoleId { get; set; }

       public virtual Role Role_RoleId { get; set; }

       [DisplayName("Currency")]
       public int? CurrencyId { get; set; }

       public virtual Currency Currency_CurrencyId { get; set; }

       [DisplayName("Country")]
       public int? CountryId { get; set; }

       public virtual Country Country_CountryId { get; set; }

       public virtual ICollection<RoleUser> RoleUsers { get; set; }

       public virtual ICollection<MenuPermission> MenuPermissions { get; set; }

       public virtual ICollection<ClientProduct> ClientProducts { get; set; }

       public virtual ICollection<Invoice> Invoices { get; set; }

       public virtual ICollection<ClientCategory> ClientCategorys { get; set; }


    }
}

