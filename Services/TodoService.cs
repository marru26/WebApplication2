using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class TodoService : ITodoService
    {
        private readonly List<TodoItem> _todoItems;

        public TodoService()
        {
            _todoItems = new List<TodoItem>
            {
                new TodoItem { Id = Guid.NewGuid(), Title = "Belajar ASP.NET Core", IsCompleted = false, DueDate = DateTime.Now.AddDays(7) },
                new TodoItem { Id = Guid.NewGuid(), Title = "Membuat Aplikasi Todo List", IsCompleted = true, DueDate = DateTime.Now.AddDays(-2) },
                new TodoItem { Id = Guid.NewGuid(), Title = "Membaca Dokumentasi", IsCompleted = false, DueDate = DateTime.Now.AddDays(3) }
            };
        }

        public async Task<List<TodoItem>> GetAllTodosAsync()
        {
            return await Task.FromResult(_todoItems.ToList());
        }

        public async Task<TodoItem> GetTodoByIdAsync(Guid id)
        {
            return await Task.FromResult(_todoItems.FirstOrDefault(t => t.Id == id));
        }

        public async Task AddTodoAsync(TodoItem todoItem)
        {
            todoItem.Id = Guid.NewGuid();
            _todoItems.Add(todoItem);
            await Task.CompletedTask;
        }

        public async Task UpdateTodoAsync(TodoItem todoItem)
        {
            var existingTodo = _todoItems.FirstOrDefault(t => t.Id == todoItem.Id);
            if (existingTodo != null)
            {
                existingTodo.Title = todoItem.Title;
                existingTodo.IsCompleted = todoItem.IsCompleted;
                existingTodo.DueDate = todoItem.DueDate;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteTodoAsync(Guid id)
        {
            var todoToRemove = _todoItems.FirstOrDefault(t => t.Id == id);
            if (todoToRemove != null)
            {
                _todoItems.Remove(todoToRemove);
            }
            await Task.CompletedTask;
        }

        public async Task<int> GetCompletedTodoCountAsync()
        {
            return await Task.FromResult(_todoItems.Count(t => t.IsCompleted));
        }

        public async Task<int> GetPendingTodoCountAsync()
        {
            return await Task.FromResult(_todoItems.Count(t => !t.IsCompleted));
        }
    }
}