using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // GET: Todo
        public async Task<IActionResult> Index()
        {
            var todoItems = await _todoService.GetAllTodosAsync();
            return View(todoItems);
        }

        // GET: Todo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Todo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,IsCompleted,DueDate")] TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                await _todoService.AddTodoAsync(todoItem);
                return RedirectToAction(nameof(Index));
            }
            return View(todoItem);
        }

        // GET: Todo/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItem = await _todoService.GetTodoByIdAsync(id.Value);
            if (todoItem == null)
            {
                return NotFound();
            }
            return View(todoItem);
        }

        // POST: Todo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,IsCompleted,DueDate")] TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _todoService.UpdateTodoAsync(todoItem);
                }
                catch (Exception)
                {
                    if (await _todoService.GetTodoByIdAsync(todoItem.Id) == null)
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
            return View(todoItem);
        }

        // GET: Todo/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItem = await _todoService.GetTodoByIdAsync(id.Value);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // POST: Todo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _todoService.DeleteTodoAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Todo/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            var completedCount = await _todoService.GetCompletedTodoCountAsync();
            var pendingCount = await _todoService.GetPendingTodoCountAsync();

            var viewModel = new TodoDashboardViewModel
            {
                CompletedCount = completedCount,
                PendingCount = pendingCount
            };

            return View(viewModel);
        }    }
}