using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientSuite.Models;  
using Microsoft.EntityFrameworkCore;
namespace ClientSuite.Data
{
    public class RoleUserMap
    {
        public RoleUserMap(EntityTypeBuilder<RoleUser> tb)
        {
            tb.HasKey(o => o.Id);
            tb.HasOne(c => c.Role_RoleId).WithMany(o => o.RoleUsers).HasForeignKey(o => o.RoleId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            tb.HasOne(c => c.User_UserId).WithMany(o => o.RoleUsers).HasForeignKey(o => o.UserId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);

            tb.ToTable("RoleUser", schema: "Identity");
        } 
    }
}
