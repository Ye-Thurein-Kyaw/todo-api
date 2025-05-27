using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; } = null!;
        public DbSet<HeroItem> HeroItems { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        // public DbSet<TodoItemDTO> TodoItemDTOs { get; set; } = null!;
        // public DbSet<HeroItemDTO> HeroItemDTOs { get; set; } = null!;


        
    }
    
}