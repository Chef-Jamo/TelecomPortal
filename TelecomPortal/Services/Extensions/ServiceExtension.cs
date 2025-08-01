using TelecomPortal.Services.Interfaces;

namespace TelecomPortal.Services.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerAccountService, CustomerAccountService>();

            return services;
        }
    }
}
