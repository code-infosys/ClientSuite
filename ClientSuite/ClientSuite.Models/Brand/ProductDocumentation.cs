using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace ClientSuite.Models
{
    public class ProductDocumentation : BaseEntity
    { 
       [Required]
       [DisplayName("Details")]
       public string Details { get; set; }

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

       [DisplayName("Title")]
       [StringLength(500)] 
       public string Title { get; set; }

       [DisplayName("Product")]
       public int? ProductId { get; set; }

       public virtual Product Product_ProductId { get; set; }

       [DisplayName("Parent")]
       public Nullable<int> ParentId { get; set; }

       public virtual ProductDocumentation ProductDocumentation2 { get; set; }

       public virtual ICollection<ProductDocumentation> ProductDocumentations { get; set; }


    }
}

