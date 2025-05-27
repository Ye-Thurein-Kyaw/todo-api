using TodoApi.Models;

namespace TodoApi.Repositories
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        Task<IEnumerable<Customer>> SearchCustomer(string searchName);
        Task<IEnumerable<CustomerResult>> ListCustomer();
        Task<IEnumerable<Customer>> SearchCustomerMultiple(CustomerResult SearchObj);
        bool IsExists(long id);
    }
}