using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientSuite.Models;  
using Microsoft.EntityFrameworkCore;
namespace ClientSuite.Data
{
    public class NotificationMap
    {
        public NotificationMap(EntityTypeBuilder<Notification> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.TableName).HasMaxLength(100);
            tb.Property(o => o.Details).HasMaxLength(300);
            tb.Property(o => o.ProcessToUrl).HasMaxLength(400);
            tb.Property(o => o.UniqueId).HasMaxLength(50);

            tb.ToTable("Notification", schema: "Identity");
        } 
    }
}
