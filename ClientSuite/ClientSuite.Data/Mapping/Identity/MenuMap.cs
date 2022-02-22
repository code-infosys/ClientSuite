using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientSuite.Models;  
using Microsoft.EntityFrameworkCore;
namespace ClientSuite.Data
{
    public class MenuMap
    {
        public MenuMap(EntityTypeBuilder<Menu> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.MenuText).HasMaxLength(100);
            tb.Property(o => o.MenuURL).HasMaxLength(400);
            tb.Property(o => o.MenuIcon).HasMaxLength(100);
            tb.HasOne(c => c.Menu2).WithMany(o => o.Menus).HasForeignKey(o => o.ParentId).OnDelete(DeleteBehavior.Restrict);

            tb.ToTable("Menu", schema: "Identity");
        } 
    }
}
