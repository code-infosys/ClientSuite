using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace ClientSuite.Models
{
    public class Ticket : BaseEntity
    { 
       [Required]
       [DisplayName("Ticket Subject")]
       [StringLength(500)] 
       public string TicketSubject { get; set; }

       [Required]
       [DisplayName("Ticket Body")]
       public string TicketBody { get; set; }

       [Required]
       [DisplayName("Added By")]
       public int AddedBy { get; set; }

       [Required]
       [DisplayName("Date Added")]
       [Column(TypeName = "datetime")]
       public DateTime DateAdded { get; set; }

       [DisplayName("Modified By")]
       public int? ModifiedBy { get; set; }

       [DisplayName("Attachment")]
       [StringLength(100)] 
       public string Attachment { get; set; }

       [Required]
       [DisplayName("Is Close")]
       public bool IsClose { get; set; }

       [DisplayName("Date Modified")]
       [Column(TypeName = "datetime")]
       public DateTime? DateModified { get; set; }

       [Required]
       [DisplayName("Is Knowledge Base")]
       public bool IsKnowledgeBase { get; set; }

       [DisplayName("Product")]
       public int? ProductId { get; set; }

       public virtual Product Product_ProductId { get; set; }

       [DisplayName("Parent")]
       public Nullable<int> ParentId { get; set; }

       public virtual Ticket Ticket2 { get; set; }

       [Required]
       [DisplayName("Priority")]
       public int PriorityId { get; set; }

       public virtual Priority Priority_PriorityId { get; set; }

       public virtual ICollection<Ticket> Tickets { get; set; }


    }
}

