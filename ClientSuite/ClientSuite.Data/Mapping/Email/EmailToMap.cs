using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientSuite.Models;  
using Microsoft.EntityFrameworkCore;
namespace ClientSuite.Data
{
    public class EmailToMap
    {
        public EmailToMap(EntityTypeBuilder<EmailTo> tb)
        {
            tb.HasKey(o => o.Id);

            tb.ToTable("EmailTo", schema: "Email");
        } 
    }
}
