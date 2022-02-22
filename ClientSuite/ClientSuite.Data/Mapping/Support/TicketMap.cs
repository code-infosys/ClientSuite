using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientSuite.Models;  
using Microsoft.EntityFrameworkCore;
namespace ClientSuite.Data
{
    public class TicketMap
    {
        public TicketMap(EntityTypeBuilder<Ticket> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.TicketSubject).HasMaxLength(500);
            tb.Property(o => o.TicketBody);
            tb.Property(o => o.Attachment).HasMaxLength(100);
            tb.HasOne(c => c.Product_ProductId).WithMany(o => o.Tickets).HasForeignKey(o => o.ProductId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            tb.HasOne(c => c.Ticket2).WithMany(o => o.Tickets).HasForeignKey(o => o.ParentId).OnDelete(DeleteBehavior.Restrict);
            tb.HasOne(c => c.Priority_PriorityId).WithMany(o => o.Tickets).HasForeignKey(o => o.PriorityId).IsRequired(true).OnDelete(DeleteBehavior.Restrict);

            tb.ToTable("Ticket", schema: "Support");
        } 
    }
}
