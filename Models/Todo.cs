namespace TodoApp.Models;
public class Todo
{
  public int Id { get; set; }
  public required string Title { get; set; }
  public bool IsComplete { get; set; }
}
