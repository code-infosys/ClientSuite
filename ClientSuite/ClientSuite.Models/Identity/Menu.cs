using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace ClientSuite.Models
{
    public class Menu : BaseEntity
    { 
       [Required]
       [DisplayName("Menu Text")]
       [StringLength(100)] 
       public string MenuText { get; set; }

       [Required]
       [DisplayName("Menu U R L")]
       [StringLength(400)] 
       public string MenuURL { get; set; }

       [DisplayName("Sort Order")]
       public int? SortOrder { get; set; }

       [DisplayName("Menu Icon")]
       [StringLength(100)] 
       public string MenuIcon { get; set; }

       [DisplayName("Parent")]
       public Nullable<int> ParentId { get; set; }

       public virtual Menu Menu2 { get; set; }

       public virtual ICollection<MenuPermission> MenuPermissions { get; set; }

       public virtual ICollection<Menu> Menus { get; set; }


    }
}

