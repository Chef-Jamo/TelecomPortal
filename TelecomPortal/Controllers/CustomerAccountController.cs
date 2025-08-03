using Microsoft.AspNetCore.Mvc;
using TelecomPortal.Data.Repository.Entities;
using TelecomPortal.Services.Dtos;
using TelecomPortal.Services.Interfaces;

namespace TelecomPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerAccountController : ControllerBase
    {
        private readonly ICustomerAccountService _customerAccountService;

        public CustomerAccountController(ICustomerAccountService customerAccountService)
        {
            _customerAccountService = customerAccountService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerAccountDto>>> GetAll()
        {
            var customers = await _customerAccountService.GetAllAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerAccountDto>> GetById(Guid id)
        {
            var customer = await _customerAccountService.GetByIdAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerAccountDto>> Create([FromBody] CustomerAccountDto customer)
        {
            var created = await _customerAccountService.CreateAsync(customer);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CustomerAccountDto updatedCustomer)
        {
            var result = await _customerAccountService.UpdateAsync(updatedCustomer);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _customerAccountService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
