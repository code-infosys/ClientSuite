using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientSuite.Models;  
using Microsoft.EntityFrameworkCore;
namespace ClientSuite.Data
{
    public class CurrencyMap
    {
        public CurrencyMap(EntityTypeBuilder<Currency> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Name).HasMaxLength(100);
            tb.Property(o => o.Code).HasMaxLength(10);

            tb.ToTable("Currency", schema: "Master");
        } 
    }
}
