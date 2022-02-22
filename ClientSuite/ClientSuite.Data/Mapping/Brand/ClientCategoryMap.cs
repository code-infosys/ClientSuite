using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientSuite.Models;  
using Microsoft.EntityFrameworkCore;
namespace ClientSuite.Data
{
    public class ClientCategoryMap
    {
        public ClientCategoryMap(EntityTypeBuilder<ClientCategory> tb)
        {
            tb.HasKey(o => o.Id);
            tb.HasOne(c => c.User_UserId).WithMany(o => o.ClientCategorys).HasForeignKey(o => o.UserId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            tb.HasOne(c => c.Category_CategoryId).WithMany(o => o.ClientCategorys).HasForeignKey(o => o.CategoryId).IsRequired(true).OnDelete(DeleteBehavior.Restrict);

            tb.ToTable("ClientCategory", schema: "Brand");
        } 
    }
}
