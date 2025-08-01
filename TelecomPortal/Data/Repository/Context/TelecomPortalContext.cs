using Microsoft.EntityFrameworkCore;
using TelecomPortal.Data.Repository.Entities;
using TelecomPortal.Data.Repository.EntitiesConfig;

namespace TelecomPortal.Data.Repository.Context
{
    public class TelecomPortalContext : DbContext
    {
        public TelecomPortalContext(DbContextOptions<TelecomPortalContext> options)
            : base(options)
        {
        }

        public DbSet<CustomerAccount> CustomerAccounts { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CustomerAccountConfig());
        }
    }
}
