using TodoApi.Models;

namespace TodoApi.Repositories
{
    public interface ITodoRepository : IRepositoryBase<TodoItem>
    {
        Task<IEnumerable<TodoItem>> SearchTodo(string searchName);
        Task<IEnumerable<TodoItem>> SearchTodoMultiple(TodoSearchPayload SearchObj);
        bool IsExists(long id);
    }
}