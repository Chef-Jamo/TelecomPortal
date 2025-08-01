using TelecomPortal.Data.Repository.Interfaces;
using TelecomPortal.Data.Repository;

namespace TelecomPortal.Data.Extensions
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICustomerAccountRepository, CustomerAccountRepository>();

            return services;
        }
    }
}
