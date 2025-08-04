using Moq;
using TelecomPortal.Data.Repository.Entities;
using TelecomPortal.Data.Repository.Interfaces;
using TelecomPortal.Services;

namespace TelecomPortal.Tests
{
    using Xunit;
    using Moq;
    using FluentAssertions;
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Linq;
    using TelecomPortal.Services.Dtos;

    public class CustomerAccountServiceTests
    {
        private readonly Mock<ICustomerAccountRepository> _mockRepo;
        private readonly CustomerAccountService _service;

        public CustomerAccountServiceTests()
        {
            _mockRepo = new Mock<ICustomerAccountRepository>();
            _service = new CustomerAccountService(_mockRepo.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnCreatedDto()
        {
            var dto = new CustomerAccountDto { Id = 1, FullName = "John Doe" };
            var entity = (CustomerAccount)dto;
            _mockRepo.Setup(r => r.AddAsync(It.IsAny<CustomerAccount>()))
                     .ReturnsAsync(entity);

            var result = await _service.CreateAsync(dto);

            result.Should().NotBeNull();
            result.Id.Should().Be(dto.Id);
            result.FullName.Should().Be("John Doe");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCustomer_WhenExists()
        {
            var id = 1;
            var entity = new CustomerAccount { Id = id, FullName = "Jane Smith" };
            _mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(entity);

            var result = await _service.GetByIdAsync(id);

            result.Should().NotBeNull();
            result.Id.Should().Be(id);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNewDto_WhenNotFound()
        {
            var id = 1;
            _mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((CustomerAccount?)null);

            var result = await _service.GetByIdAsync(id);

            result.Should().NotBeNull();
            result.Should().BeOfType<CustomerAccountDto>();
            result.Id.Should().Be(0);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnMappedList()
        {
            var list = new List<CustomerAccount>
            {
                new() { Id = 1, FullName = "Alice" },
                new() { Id = 2, FullName = "Bob" }
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(list);

            var result = await _service.GetAllAsync();

            result.Should().HaveCount(2);
            result.Select(x => x.FullName).Should().Contain(new[] { "Alice", "Bob" });
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnUpdatedDto()
        {
            var dto = new CustomerAccountDto { Id = 1, FullName = "Updated Name" };
            var entity = (CustomerAccount)dto;
            _mockRepo.Setup(r => r.UpdateAsync(It.IsAny<CustomerAccount>()))
                     .ReturnsAsync(entity);

            var result = await _service.UpdateAsync(dto);

            result.Should().NotBeNull();
            result.FullName.Should().Be("Updated Name");
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenDeleted()
        {
            var id = 1;
            _mockRepo.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);

            var result = await _service.DeleteAsync(id);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenNotFound()
        {
            var id = 1;
            _mockRepo.Setup(r => r.DeleteAsync(id)).ReturnsAsync(false);

            var result = await _service.DeleteAsync(id);

            result.Should().BeFalse();
        }
    }

}
