using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientSuite.Models;  
using Microsoft.EntityFrameworkCore;
namespace ClientSuite.Data
{
    public class RoleMap
    {
        public RoleMap(EntityTypeBuilder<Role> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.RoleName).HasMaxLength(50);

            tb.ToTable("Role", schema: "Identity");
        } 
    }
}
