using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CustomerController(IRepositoryWrapper RW)
        {
            _repositoryWrapper = RW;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerRequest>>> GetCustomers()
        {
          var customers =  await _repositoryWrapper.Customer.ListCustomer();
            return Ok(customers);
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerRequest>> GetCustomer(long id)
        {
          
            var customer = await _repositoryWrapper.Customer.FindByIDAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return CustomerToDTO(customer);
        }

        // PUT: api/Customer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(long id, CustomerRequest customerRequest)
        {
            if (id != customerRequest.CustomerId)
            {
                return BadRequest();
            }

            var customer = await _repositoryWrapper.Customer.FindByIDAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.CustomerName = customerRequest.CustomerName;
            customer.CustomerAddress = customerRequest.CustomerAddress;
            customer.CusCustomerTypeId = customerRequest.CusCustomerTypeId;

            try
            {
                await _repositoryWrapper.Customer.UpdateAsync(customer);
            }
            catch (DbUpdateConcurrencyException) when (!CustomerExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Customer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerRequest>> PostCustomer(CustomerRequest customerRequest)
        {
          var customer = new Customer
            {
                CustomerName = customerRequest.CustomerName,
                RegisterDate = DateTime.Now,
                CustomerAddress = customerRequest.CustomerAddress,
                CusCustomerTypeId = customerRequest.CusCustomerTypeId
            };

        
            await _repositoryWrapper.Customer.CreateAsync(customer, true);

            return CreatedAtAction(
                nameof(GetCustomer),
                new { id = customer.CustomerId },
                CustomerToDTO(customer));
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(long id)
        {
            if (_repositoryWrapper.Customer == null)
            {
                return NotFound();
            }
            var customer = await _repositoryWrapper.Customer.FindByIDAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            await _repositoryWrapper.Customer.DeleteAsync(customer, true);

            return NoContent();
        }

        [HttpPost("search/{term}")]
        public async Task<ActionResult<IEnumerable<CustomerRequest>>>  SearchCustomer(string term)
        {
            var empList = await _repositoryWrapper.Customer.SearchCustomer(term);
            return Ok(empList);           
        }

        [HttpPost("searchcustomer")]
        public async Task<ActionResult<IEnumerable<CustomerRequest>>>  SearchCustomerMultiple(CustomerResult SearchObj)
        {
            var empList = await _repositoryWrapper.Customer.SearchCustomerMultiple(SearchObj);
            return Ok(empList);           
        }

        private bool CustomerExists(long id)
        {
            return _repositoryWrapper.Customer.IsExists(id);
        }

        private static CustomerRequest CustomerToDTO(Customer customer) =>
            new CustomerRequest
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                RegisterDate = customer.RegisterDate,
                CustomerAddress = customer.CustomerAddress,
                CusCustomerTypeId = customer.CusCustomerTypeId,
            };
    }
}
