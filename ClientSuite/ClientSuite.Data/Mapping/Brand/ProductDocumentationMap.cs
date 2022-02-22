using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientSuite.Models;  
using Microsoft.EntityFrameworkCore;
namespace ClientSuite.Data
{
    public class ProductDocumentationMap
    {
        public ProductDocumentationMap(EntityTypeBuilder<ProductDocumentation> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Details);
            tb.Property(o => o.Title).HasMaxLength(500);
            tb.HasOne(c => c.Product_ProductId).WithMany(o => o.ProductDocumentations).HasForeignKey(o => o.ProductId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            tb.HasOne(c => c.ProductDocumentation2).WithMany(o => o.ProductDocumentations).HasForeignKey(o => o.ParentId).OnDelete(DeleteBehavior.Restrict);

            tb.ToTable("ProductDocumentation", schema: "Brand");
        } 
    }
}
