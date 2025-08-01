using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TelecomPortal.Data.Repository.Entities;

namespace TelecomPortal.Data.Repository.EntitiesConfig
{
    public class CustomerAccountConfig : IEntityTypeConfiguration<CustomerAccount>
    {
        public void Configure(EntityTypeBuilder<CustomerAccount> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.FullName).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Email).HasMaxLength(150);
            builder.Property(e => e.PhoneNumber).HasMaxLength(20);
        }
    }
}
