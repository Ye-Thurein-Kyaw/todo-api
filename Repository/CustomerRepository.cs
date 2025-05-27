using System.Data;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TodoApi.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(TodoContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<Customer>> SearchCustomer(string searchTerm)
        {
            return await RepositoryContext.Customers
                        .Where(s => s.CustomerName.Contains(searchTerm))
                        .OrderBy(s => s.CustomerId).ToListAsync();
        }

        public async Task<IEnumerable<CustomerResult>> ListCustomer()
        {
            // return await RepositoryContext.Customers
            //             .OrderBy(s => s.Id).ToListAsync();
            // return await RepositoryContext.Customers
            //             .Include(e => e.EmpDepartment)
            //             .OrderBy(s => s.Id).ToListAsync();
            // return await RepositoryContext.Customers
            //             .Select(e => new CustomerResult{
            //                 CustomerId = e.CustomerId,
            //                 CustomerName = e.CustomerName,
            //                 CustomerAddress = e.CustomerAddress,
            //                 CusCustomerTypeId = e.CusCustomerTypeId
            //             })
            //             .OrderBy(s => s.CustomerId).ToListAsync();
            return await RepositoryContext.Customers
                        .Select(e => new CustomerResult{
                            CustomerId = e.CustomerId,
                            CustomerName = e.CustomerName,
                            RegisterDate = e.RegisterDate, 
                            CustomerAddress = e.CustomerAddress,
                            CusCustomerTypeId = e.CusCustomerTypeId,
                            CusCustomerTypeName = e.CusCustomerType!.CustomerTypeName
                        })
                        .OrderBy(s => s.CustomerId).ToListAsync();
        }


        public bool IsExists(long id)
        {
            return RepositoryContext.Customers.Any(e => e.CustomerId == id);
        }

        public Task<IEnumerable<CustomerResult>> ListCustomer(string searchName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> SearchCustomerMultiple(CustomerResult SearchObj)
        {
            throw new NotImplementedException();
        }
    }

}