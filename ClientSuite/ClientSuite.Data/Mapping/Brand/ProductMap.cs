using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientSuite.Models;  
using Microsoft.EntityFrameworkCore;
namespace ClientSuite.Data
{
    public class ProductMap
    {
        public ProductMap(EntityTypeBuilder<Product> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Name).HasMaxLength(500);
            tb.Property(o => o.Picture).HasMaxLength(100);
            tb.Property(o => o.Description);

            tb.ToTable("Product", schema: "Brand");
        } 
    }
}
