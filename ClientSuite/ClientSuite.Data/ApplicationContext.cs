using Microsoft.EntityFrameworkCore;
using ClientSuite.Models;

namespace ClientSuite.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<RoleUser> RoleUser { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuPermission> MenuPermission { get; set; }
        public DbSet<AppSetting> AppSetting { get; set; }
        public DbSet<GeneralSetting> GeneralSetting { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<ClientProduct> ClientProduct { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<PurchaseStatus> PurchaseStatus { get; set; }
        public DbSet<ClientCategory> ClientCategory { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Priority> Priority { get; set; }
        public DbSet<EmailMessage> EmailMessage { get; set; }
        public DbSet<EmailTo> EmailTo { get; set; }
        public DbSet<ProductDocumentation> ProductDocumentation { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {  
            new RoleMap(modelBuilder.Entity<Role>());
            new UserMap(modelBuilder.Entity<User>());
            new RoleUserMap(modelBuilder.Entity<RoleUser>());
            new MenuMap(modelBuilder.Entity<Menu>());
            new MenuPermissionMap(modelBuilder.Entity<MenuPermission>());
            new AppSettingMap(modelBuilder.Entity<AppSetting>());
            new GeneralSettingMap(modelBuilder.Entity<GeneralSetting>());
            new NotificationMap(modelBuilder.Entity<Notification>());
            new CurrencyMap(modelBuilder.Entity<Currency>());
            new CountryMap(modelBuilder.Entity<Country>());
            new ClientProductMap(modelBuilder.Entity<ClientProduct>());
            new ProductMap(modelBuilder.Entity<Product>());
            new PurchaseStatusMap(modelBuilder.Entity<PurchaseStatus>());
            new ClientCategoryMap(modelBuilder.Entity<ClientCategory>());
            new CategoryMap(modelBuilder.Entity<Category>());
            new InvoiceMap(modelBuilder.Entity<Invoice>());
            new ContactsMap(modelBuilder.Entity<Contacts>());
            new TicketMap(modelBuilder.Entity<Ticket>());
            new PriorityMap(modelBuilder.Entity<Priority>());
            new EmailMessageMap(modelBuilder.Entity<EmailMessage>());
            new EmailToMap(modelBuilder.Entity<EmailTo>());
            new ProductDocumentationMap(modelBuilder.Entity<ProductDocumentation>());
            new TransactionMap(modelBuilder.Entity<Transaction>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
