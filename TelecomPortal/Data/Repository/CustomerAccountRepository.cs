using TelecomPortal.Data.Repository.Base;
using TelecomPortal.Data.Repository.Context;
using TelecomPortal.Data.Repository.Entities;
using TelecomPortal.Data.Repository.Interfaces;

namespace TelecomPortal.Data.Repository
{
    public class CustomerAccountRepository : BaseRepository<CustomerAccount>, ICustomerAccountRepository
    {
        public CustomerAccountRepository(TelecomPortalContext context)
            : base(context)
        {
        }
    }
}
