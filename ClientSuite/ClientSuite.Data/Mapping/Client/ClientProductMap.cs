using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientSuite.Models;  
using Microsoft.EntityFrameworkCore;
namespace ClientSuite.Data
{
    public class ClientProductMap
    {
        public ClientProductMap(EntityTypeBuilder<ClientProduct> tb)
        {
            tb.HasKey(o => o.Id);
            tb.HasOne(c => c.User_UserId).WithMany(o => o.ClientProducts).HasForeignKey(o => o.UserId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            tb.HasOne(c => c.Product_ProductId).WithMany(o => o.ClientProducts).HasForeignKey(o => o.ProductId).IsRequired(true).OnDelete(DeleteBehavior.Restrict);
            tb.HasOne(c => c.PurchaseStatus_PurchaseStatusId).WithMany(o => o.ClientProducts).HasForeignKey(o => o.PurchaseStatusId).IsRequired(true).OnDelete(DeleteBehavior.Restrict);

            tb.ToTable("ClientProduct", schema: "Client");
        } 
    }
}
