using TodoApi.Models;
namespace TodoApi.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly TodoContext _repoContext;

        public RepositoryWrapper(TodoContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        private IHeroRepository? oHeroItem;
        public IHeroRepository HeroItem
        {
            get
            {
                if (oHeroItem == null)
                {
                    oHeroItem = new HeroRepository(_repoContext);
                }

                return oHeroItem;
            }
        }
        private ITodoRepository? oTodoItem;
        public ITodoRepository TodoItem
        {
            get
            {
                if (oTodoItem == null)
                {
                    oTodoItem = new TodoRepository(_repoContext);
                }

                return oTodoItem;
            }
        }
         private IEmployeeRepository? oEmployee;
        public IEmployeeRepository Employee
        {
            get
            {
                if (oEmployee == null)
                {
                    oEmployee = new EmployeeRepository(_repoContext);
                }

                return oEmployee;
            }
        }
         private ICustomerRepository? oCustomer;
        public ICustomerRepository Customer
        {
            get
            {
                if (oCustomer == null)
                {
                    oCustomer = new CustomerRepository(_repoContext);
                }

                return oCustomer;
            }
        }
    }
}