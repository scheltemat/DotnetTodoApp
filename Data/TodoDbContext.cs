using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

public class TodoDbContext : DbContext
{
  public DbSet<Todo> Todos { get; set; }
  public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }
}
