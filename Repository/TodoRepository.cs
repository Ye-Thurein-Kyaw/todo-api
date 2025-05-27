using System.Data;
using System.Linq;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Repositories
{
    public class TodoRepository : RepositoryBase<TodoItem>, ITodoRepository
    {
        public TodoRepository(TodoContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<TodoItem>> SearchTodo(string searchTerm)
        {
            return await RepositoryContext.TodoItems
                        .Where(s => s.Name.Contains(searchTerm))
                        .OrderBy(s => s.Id).ToListAsync();
        }

        public async Task<IEnumerable<TodoItem>> SearchTodoMultiple(TodoSearchPayload SearchObj)
        {
            return await RepositoryContext.TodoItems
                        .Where(s => s.Name.Contains(SearchObj.NameTerm ?? "") || s.Secret.Contains(SearchObj.SecretTerm ?? ""))
                        .OrderBy(s => s.Id).ToListAsync();
        }

        public bool IsExists(long id)
        {
            return RepositoryContext.TodoItems.Any(e => e.Id == id);
        }
    }

}