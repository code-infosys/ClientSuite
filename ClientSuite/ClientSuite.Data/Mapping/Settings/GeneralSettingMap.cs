using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientSuite.Models;  
using Microsoft.EntityFrameworkCore;
namespace ClientSuite.Data
{
    public class GeneralSettingMap
    {
        public GeneralSettingMap(EntityTypeBuilder<GeneralSetting> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.SettingKey).HasMaxLength(50);
            tb.Property(o => o.SettingValue).HasMaxLength(2000);
            tb.Property(o => o.Description).HasMaxLength(100);
            tb.Property(o => o.SettingGroup).HasMaxLength(50);
            tb.Property(o => o.FieldType).HasMaxLength(100);

            tb.ToTable("GeneralSetting", schema: "Settings");
        } 
    }
}
