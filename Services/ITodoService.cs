using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication2.Services
{
    public interface ITodoService
    {
        Task<List<Models.TodoItem>> GetAllTodosAsync();
        Task<Models.TodoItem> GetTodoByIdAsync(Guid id);
        Task AddTodoAsync(Models.TodoItem todoItem);
        Task UpdateTodoAsync(Models.TodoItem todoItem);
        Task DeleteTodoAsync(Guid id);
        Task<int> GetCompletedTodoCountAsync();
        Task<int> GetPendingTodoCountAsync();
    }
}