using TelecomPortal.Data.Repository.Entities.Base;
using TelecomPortal.Services.Dtos;

namespace TelecomPortal.Data.Repository.Entities
{
    public class CustomerAccount : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public static implicit operator CustomerAccountDto(CustomerAccount entity)
        {
            return new CustomerAccountDto
            {
                Id = entity.Id,
                FullName = entity.FullName,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                IsActive = entity.IsActive
            };
        }
    }
}
