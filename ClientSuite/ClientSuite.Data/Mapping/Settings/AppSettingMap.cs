using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientSuite.Models;  
using Microsoft.EntityFrameworkCore;
namespace ClientSuite.Data
{
    public class AppSettingMap
    {
        public AppSettingMap(EntityTypeBuilder<AppSetting> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.AppName).HasMaxLength(100);
            tb.Property(o => o.AppShortName).HasMaxLength(50);
            tb.Property(o => o.AppVersion).HasMaxLength(15);
            tb.Property(o => o.Skin).HasMaxLength(20);
            tb.Property(o => o.FooterText).HasMaxLength(150);
            tb.Property(o => o.Logo).HasMaxLength(100);
            tb.Property(o => o.LoginPageBackground).HasMaxLength(100);
            tb.Property(o => o.TimeZone).HasMaxLength(200);
            tb.Property(o => o.Currency).HasMaxLength(20);

            tb.ToTable("AppSetting", schema: "Settings");
        } 
    }
}
