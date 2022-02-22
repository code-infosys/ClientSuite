using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace ClientSuite.Models
{
    public class Country : BaseEntity
    { 
       [Required]
       [DisplayName("Name")]
       [StringLength(100)] 
       public string Name { get; set; }

       [Required]
       [DisplayName("Code")]
       [StringLength(10)] 
       public string Code { get; set; }

       public virtual ICollection<User> Users { get; set; }


    }
}

