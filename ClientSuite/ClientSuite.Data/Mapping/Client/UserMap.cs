using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientSuite.Models;  
using Microsoft.EntityFrameworkCore;
namespace ClientSuite.Data
{
    public class UserMap
    {
        public UserMap(EntityTypeBuilder<User> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.FullName).HasMaxLength(300);
            tb.Property(o => o.Email).HasMaxLength(50);
            tb.Property(o => o.MobileNumber).HasMaxLength(30);
            tb.Property(o => o.ChangePasswordCode).HasMaxLength(100);
            tb.Property(o => o.Otp).HasMaxLength(20);
            tb.Property(o => o.ProfilePicture).HasMaxLength(100);
            tb.Property(o => o.UserName).HasMaxLength(100);
            tb.Property(o => o.Password).HasMaxLength(500);
            tb.Property(o => o.CompanyName).HasMaxLength(200);
            tb.Property(o => o.CompanyPhone).HasMaxLength(20);
            tb.Property(o => o.CompanyMobile).HasMaxLength(20);
            tb.Property(o => o.CompanyAddress).HasMaxLength(1000);
            tb.HasOne(c => c.Role_RoleId).WithMany(o => o.Users).HasForeignKey(o => o.RoleId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            tb.HasOne(c => c.Currency_CurrencyId).WithMany(o => o.Users).HasForeignKey(o => o.CurrencyId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            tb.HasOne(c => c.Country_CountryId).WithMany(o => o.Users).HasForeignKey(o => o.CountryId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

            tb.ToTable("User", schema: "Client");
        } 
    }
}
