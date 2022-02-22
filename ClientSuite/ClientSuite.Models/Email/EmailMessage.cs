using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ClientSuite.Models
{
    public class EmailMessage : BaseEntity
    { 
       [Required]
       [DisplayName("From User")]
       public int FromUserId { get; set; }

       [DisplayName("Subject")]
       [StringLength(1000)] 
       public string Subject { get; set; }

       [Required]
       [DisplayName("Body")]
       public string Body { get; set; }

       [Required]
       [DisplayName("Sent Date")]
       [Column(TypeName = "datetime")]
       public DateTime SentDate { get; set; }

       [DisplayName("Attachment Json")]
       [StringLength(2000)] 
       public string AttachmentJson { get; set; }

       [NotMapped]
       public List<Attachments> AttachmentJsonList
       {
            get => !string.IsNullOrEmpty(AttachmentJson) ? JsonConvert.DeserializeObject<List<Attachments>>(AttachmentJson) : null;
       }

    }

    public class Attachments
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; } 
    }

}

