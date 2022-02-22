using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientSuite.Models;  
using Microsoft.EntityFrameworkCore;
namespace ClientSuite.Data
{
    public class TransactionMap
    {
        public TransactionMap(EntityTypeBuilder<Transaction> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Source).HasMaxLength(100);
            tb.Property(o => o.TransactionNumber).HasMaxLength(300);
            tb.HasOne(c => c.Product_ProductId).WithMany(o => o.Transactions).HasForeignKey(o => o.ProductId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

            tb.ToTable("Transaction", schema: "Payment");
        } 
    }
}
