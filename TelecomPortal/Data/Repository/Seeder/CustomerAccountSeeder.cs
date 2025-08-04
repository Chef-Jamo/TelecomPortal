using Microsoft.EntityFrameworkCore;
using TelecomPortal.Data.Repository.Context;
using TelecomPortal.Data.Repository.Entities;

namespace TelecomPortal.Data.Repository.Seeder
{
    public class CustomerAccountSeeder
    {
        private readonly TelecomPortalContext _context;

        public CustomerAccountSeeder(TelecomPortalContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (!await _context.CustomerAccounts.AnyAsync())
            {
                var items = Enumerable.Range(1, 20).Select(i => new CustomerAccount
                {
                    FullName = $"Sample Item {i}",
                    Email = $"SampleAddress{i}@gmail.com",
                    PhoneNumber = $"000-000-00{i}",
                    IsActive = true ,
                });

                await _context.CustomerAccounts.AddRangeAsync(items);
                await _context.SaveChangesAsync();
            }
        }
    }
}
