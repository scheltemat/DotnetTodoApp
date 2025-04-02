using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models;
using System.Threading.Tasks;

public class TodoController : Controller
{
  private readonly TodoDbContext _context;

  public TodoController(TodoDbContext context)
  {
    _context = context;
  }

  // GET: /Todo/
  public async Task<IActionResult> Index()
  {
    return View(await _context.Todos.ToListAsync());
  }

  // GET: /Todo/Create
  public IActionResult Create()
  {
    return View();
  }

  // POST: /Todo/Create
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create([Bind("Id,Title,IsComplete")] Todo todo)
  {
    if (ModelState.IsValid)
    {
      _context.Add(todo);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
    return View(todo);
  }

  // GET: /Todo/Edit/5
  public async Task<IActionResult> Edit(int? id)
  {
    if (id == null)
    {
      return NotFound();
    }

    var todo = await _context.Todos.FindAsync(id);
    if (todo == null)
    {
      return NotFound();
    }

    return View(todo);
  }

  // POST: /Todo/Edit/5
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(int id, [Bind("Id,Title,IsComplete")] Todo todo)
  {
    if (id != todo.Id)
    {
      return NotFound();
    }

    if (ModelState.IsValid)
    {
      try
      {
        _context.Update(todo);
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!_context.Todos.Any(e => e.Id == todo.Id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }
      return RedirectToAction(nameof(Index));
    }
    return View(todo);
  }

  // GET: /Todo/Delete/5
  public async Task<IActionResult> Delete(int? id)
  {
    if (id == null)
    {
      return NotFound();
    }

    var todo = await _context.Todos
        .FirstOrDefaultAsync(m => m.Id == id);
    if (todo == null)
    {
      return NotFound();
    }

    // Return the Delete view with the Todo item to confirm the deletion
    return View(todo);
  }

  // POST: /Todo/Delete/5
  [HttpPost, ActionName("Delete")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteConfirmed(int id)
  {
    var todo = await _context.Todos.FindAsync(id);
    if (todo != null)
    {
      _context.Todos.Remove(todo);
      await _context.SaveChangesAsync();
    }
    return RedirectToAction(nameof(Index));  // Redirect back to the list of todos
  }

}
