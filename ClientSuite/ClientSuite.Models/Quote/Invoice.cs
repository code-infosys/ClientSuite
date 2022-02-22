using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ClientSuite.Models
{
    public class Invoice : BaseEntity
    { 
       [Required]
       [DisplayName("Name")]
       [StringLength(100)] 
       public string Name { get; set; }

       [DisplayName("Business Logo")]
       [StringLength(100)] 
       public string BusinessLogo { get; set; }

       [Required]
       [DisplayName("Business Details")]
       [StringLength(1000)] 
       public string BusinessDetails { get; set; }

       [DisplayName("Bill To")]
       [StringLength(200)] 
       public string BillTo { get; set; }

       [Required]
       [DisplayName("Client Details")]
       [StringLength(1000)] 
       public string ClientDetails { get; set; }

       [Required]
       [DisplayName("Currency")]
       [StringLength(100)] 
       public string Currency { get; set; }

       [Required]
       [DisplayName("Dated")]
       [Column(TypeName = "datetime")]
       public DateTime Dated { get; set; }

       [DisplayName("Due Date")]
       [Column(TypeName = "datetime")]
       public DateTime? DueDate { get; set; }

       [Required]
       [DisplayName("Item Json")]
       public string ItemJson { get; set; }

       [NotMapped]
       public List<InvoiceItem> ItemJsonList
       {
            get => !string.IsNullOrEmpty(ItemJson) ? JsonConvert.DeserializeObject<List<InvoiceItem>>(ItemJson) : null;
       }
       [Required]
       [DisplayName("Sub Total")]
       public decimal SubTotal { get; set; }

       [Required]
       [DisplayName("Tax")]
       public decimal Tax { get; set; }

       [Required]
       [DisplayName("Total Due")]
       public decimal TotalDue { get; set; }

       [DisplayName("Paid Amount")]
       public decimal? PaidAmount { get; set; }

       [DisplayName("Balance Due")]
       public decimal? BalanceDue { get; set; }

       [Required]
       [DisplayName("Is Paid")]
       public bool IsPaid { get; set; }

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
       [DisplayName("Is Quote")]
       public bool IsQuote { get; set; }

       [DisplayName("Note")]
       [StringLength(1000)] 
       public string Note { get; set; }

       [DisplayName("Terms And Condition")]
       [StringLength(1000)] 
       public string TermsAndCondition { get; set; }

       [DisplayName("Sign")]
       [StringLength(200)] 
       public string Sign { get; set; }

       [Required]
       [DisplayName("User")]
       public int UserId { get; set; }

       public virtual User User_UserId { get; set; }


    }

    public class InvoiceItem
    {
        public string Item { get; set; }
        public string Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
    }
}

