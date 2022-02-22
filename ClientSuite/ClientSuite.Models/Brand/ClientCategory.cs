using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace ClientSuite.Models
{
    public class ClientCategory : BaseEntity
    { 
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
       [DisplayName("User")]
       public int UserId { get; set; }

       public virtual User User_UserId { get; set; }

       [Required]
       [DisplayName("Category")]
       public int CategoryId { get; set; }

       public virtual Category Category_CategoryId { get; set; }


    }
}

