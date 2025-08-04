using TelecomPortal.Data.Repository.Entities;
using TelecomPortal.Data.Repository.Interfaces;
using TelecomPortal.Services.Dtos;
using TelecomPortal.Services.Interfaces;

namespace TelecomPortal.Services
{
    public class CustomerAccountService : ICustomerAccountService
    {
        private readonly ICustomerAccountRepository _repository;

        public CustomerAccountService(ICustomerAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomerAccountDto> CreateAsync(CustomerAccountDto dto)
        {
            var created = await _repository.AddAsync(dto);
            return created;
        }

        public async Task<CustomerAccountDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity ?? new();
        }

        public async Task<IEnumerable<CustomerAccountDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(e => (CustomerAccountDto)e);
        }

        public async Task<CustomerAccountDto> UpdateAsync(CustomerAccountDto dto)
        {
            var updated = await _repository.UpdateAsync(dto);
            return updated;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }

}
