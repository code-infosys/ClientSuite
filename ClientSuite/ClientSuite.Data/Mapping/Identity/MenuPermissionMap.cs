using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientSuite.Models;  
using Microsoft.EntityFrameworkCore;
namespace ClientSuite.Data
{
    public class MenuPermissionMap
    {
        public MenuPermissionMap(EntityTypeBuilder<MenuPermission> tb)
        {
            tb.HasKey(o => o.Id);
            tb.HasOne(c => c.Role_RoleId).WithMany(o => o.MenuPermissions).HasForeignKey(o => o.RoleId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            tb.HasOne(c => c.User_UserId).WithMany(o => o.MenuPermissions).HasForeignKey(o => o.UserId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
            tb.HasOne(c => c.Menu_MenuId).WithMany(o => o.MenuPermissions).HasForeignKey(o => o.MenuId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);

            tb.ToTable("MenuPermission", schema: "Identity");
        } 
    }
}
