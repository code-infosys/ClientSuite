using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientSuite.Models;  
using Microsoft.EntityFrameworkCore;
namespace ClientSuite.Data
{
    public class InvoiceMap
    {
        public InvoiceMap(EntityTypeBuilder<Invoice> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Name).HasMaxLength(100);
            tb.Property(o => o.BusinessLogo).HasMaxLength(100);
            tb.Property(o => o.BusinessDetails).HasMaxLength(1000);
            tb.Property(o => o.BillTo).HasMaxLength(200);
            tb.Property(o => o.ClientDetails).HasMaxLength(1000);
            tb.Property(o => o.Currency).HasMaxLength(20);
            tb.Property(o => o.ItemJson);
            tb.Property(o => o.Note).HasMaxLength(1000);
            tb.Property(o => o.TermsAndCondition).HasMaxLength(1000);
            tb.Property(o => o.Sign).HasMaxLength(200);
            tb.HasOne(c => c.User_UserId).WithMany(o => o.Invoices).HasForeignKey(o => o.UserId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);

            tb.ToTable("Invoice", schema: "Quote");
        } 
    }
}
