using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace ClientSuite.Models
{
    public class ClientProduct : BaseEntity
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

       [DisplayName("Follow Up Date")]
       [Column(TypeName = "datetime")]
       public DateTime? FollowUpDate { get; set; }

       [Required]
       [DisplayName("User")]
       public int UserId { get; set; }

       public virtual User User_UserId { get; set; }

       [Required]
       [DisplayName("Product")]
       public int ProductId { get; set; }

       public virtual Product Product_ProductId { get; set; }

       [Required]
       [DisplayName("Purchase Status")]
       public int PurchaseStatusId { get; set; }

       public virtual PurchaseStatus PurchaseStatus_PurchaseStatusId { get; set; }


    }
}

