using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace ClientSuite.Models
{
    public class Contacts : BaseEntity
    { 
       [DisplayName("Email")]
       [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
       [StringLength(200)] 

       public string Email { get; set; }

       [DisplayName("Mobile")]
       [StringLength(20)] 
       public string Mobile { get; set; }

       [DisplayName("Added By")]
       public int? AddedBy { get; set; }

       [DisplayName("Date Added")]
       [Column(TypeName = "datetime")]
       public DateTime? DateAdded { get; set; }

       [DisplayName("Modified By")]
       public int? ModifiedBy { get; set; }

       [DisplayName("Date Modified")]
       [Column(TypeName = "datetime")]
       public DateTime? DateModified { get; set; }

       [Required]
       [DisplayName("Name")]
       [StringLength(200)] 
       public string Name { get; set; }

       [DisplayName("Address")]
       [StringLength(500)] 
       public string Address { get; set; }

       [Required]
       [DisplayName("Category")]
       public int CategoryId { get; set; }

       public virtual Category Category_CategoryId { get; set; }


    }
}

