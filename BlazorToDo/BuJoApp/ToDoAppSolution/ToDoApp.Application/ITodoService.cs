// using と namespace が正しいか確認
using ToDoApp.Domain;

namespace ToDoApp.Application
{
    public interface ITodoService
    {
        Task<List<TodoItem>> GetTodosAsync();
        Task SaveTodosAsync(List<TodoItem> todos);
    }
}