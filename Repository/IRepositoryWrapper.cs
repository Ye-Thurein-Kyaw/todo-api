namespace TodoApi.Repositories
{
    public interface IRepositoryWrapper
    {
        IHeroRepository HeroItem { get; }
        ITodoRepository TodoItem { get; }
        IEmployeeRepository Employee { get;}
        ICustomerRepository Customer { get;}
    }
       
}


