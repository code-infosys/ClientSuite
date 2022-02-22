using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace ClientSuite.Models
{
    public class Transaction : BaseEntity
    { 
       [Required]
       [DisplayName("Source")]
       [StringLength(100)] 
       public string Source { get; set; }

       [Required]
       [DisplayName("Amount")]
       public decimal Amount { get; set; }

       [Required]
       [DisplayName("Transaction Number")]
       [StringLength(300)] 
       public string TransactionNumber { get; set; }

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

       [DisplayName("Product")]
       public int? ProductId { get; set; }

       public virtual Product Product_ProductId { get; set; }


    }
}

