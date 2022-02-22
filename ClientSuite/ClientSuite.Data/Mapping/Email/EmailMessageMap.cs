using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientSuite.Models;  
using Microsoft.EntityFrameworkCore;
namespace ClientSuite.Data
{
    public class EmailMessageMap
    {
        public EmailMessageMap(EntityTypeBuilder<EmailMessage> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Subject).HasMaxLength(1000);
            tb.Property(o => o.Body);
            tb.Property(o => o.AttachmentJson).HasMaxLength(2000);

            tb.ToTable("EmailMessage", schema: "Email");
        } 
    }
}
