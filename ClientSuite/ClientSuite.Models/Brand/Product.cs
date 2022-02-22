using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace ClientSuite.Models
{
    public class Product : BaseEntity
    { 
       [Required]
       [DisplayName("Is Active")]
       public bool IsActive { get; set; }

       [Required]
       [DisplayName("Name")]
       [StringLength(500)] 
       public string Name { get; set; }

       [DisplayName("Picture")]
       [StringLength(100)] 
       public string Picture { get; set; }

       [Required]
       [DisplayName("Price")]
       public decimal Price { get; set; }

       [DisplayName("Description")]
       public string Description { get; set; }

       public virtual ICollection<ProductDocumentation> ProductDocumentations { get; set; }

       public virtual ICollection<ClientProduct> ClientProducts { get; set; }

       public virtual ICollection<Ticket> Tickets { get; set; }

       public virtual ICollection<Transaction> Transactions { get; set; }


    }
}

