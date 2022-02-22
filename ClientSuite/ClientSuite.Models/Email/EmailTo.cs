using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace ClientSuite.Models
{
    public class EmailTo : BaseEntity
    { 
       [Required]
       [DisplayName("Email Message")]
       public int EmailMessageId { get; set; }

       [Required]
       [DisplayName("To User")]
       public int ToUserId { get; set; }

       [DisplayName("Date Created")]
       [Column(TypeName = "datetime")]
       public DateTime? DateCreated { get; set; }


    }
}

