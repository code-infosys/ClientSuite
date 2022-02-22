using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientSuite.Models;  
using Microsoft.EntityFrameworkCore;
namespace ClientSuite.Data
{
    public class CategoryMap
    {
        public CategoryMap(EntityTypeBuilder<Category> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Name).HasMaxLength(100);

            tb.ToTable("Category", schema: "Master");
        } 
    }
}
