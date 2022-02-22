using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientSuite.Models;  
using Microsoft.EntityFrameworkCore;
namespace ClientSuite.Data
{
    public class CountryMap
    {
        public CountryMap(EntityTypeBuilder<Country> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Name).HasMaxLength(100);
            tb.Property(o => o.Code).HasMaxLength(10);

            tb.ToTable("Country", schema: "Master");
        } 
    }
}
