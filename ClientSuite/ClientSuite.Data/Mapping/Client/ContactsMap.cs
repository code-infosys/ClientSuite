using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientSuite.Models;  
using Microsoft.EntityFrameworkCore;
namespace ClientSuite.Data
{
    public class ContactsMap
    {
        public ContactsMap(EntityTypeBuilder<Contacts> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Email).HasMaxLength(200);
            tb.Property(o => o.Mobile).HasMaxLength(20);
            tb.Property(o => o.Name).HasMaxLength(200);
            tb.Property(o => o.Address).HasMaxLength(500);
            tb.HasOne(c => c.Category_CategoryId).WithMany(o => o.Contactss).HasForeignKey(o => o.CategoryId).IsRequired(true).OnDelete(DeleteBehavior.Restrict);

            tb.ToTable("Contacts", schema: "Client");
        } 
    }
}
