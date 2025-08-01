using TelecomPortal.Data.Repository.Entities;
using TelecomPortal.Services.Dtos.Base;

namespace TelecomPortal.Services.Dtos
{
    public class CustomerAccountDto : BaseDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public static implicit operator CustomerAccount(CustomerAccountDto dto)
        {
            return new CustomerAccount
            {
                Id = dto.Id,
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                IsActive = dto.IsActive
            };
        }
    }
}
