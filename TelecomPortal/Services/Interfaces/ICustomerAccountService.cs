using TelecomPortal.Services.Dtos;

namespace TelecomPortal.Services.Interfaces
{
    public interface ICustomerAccountService
    {
        Task<CustomerAccountDto> CreateAsync(CustomerAccountDto dto);
        Task<CustomerAccountDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<CustomerAccountDto>> GetAllAsync();
        Task<CustomerAccountDto> UpdateAsync(CustomerAccountDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
