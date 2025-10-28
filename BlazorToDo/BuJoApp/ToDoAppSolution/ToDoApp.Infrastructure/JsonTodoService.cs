// using と namespace が正しいか確認
using System.IO;
using System.Text.Json;
using ToDoApp.Application; // Applicationを参照
using ToDoApp.Domain;      // Domainを参照

namespace ToDoApp.Infrastructure
{
    public class JsonTodoService : ITodoService
    {
        private const string FilePath = "todos.json";

        public async Task<List<TodoItem>> GetTodosAsync()
        {
            if (!File.Exists(FilePath))
            {
                return new List<TodoItem>();
            }

            var json = await File.ReadAllTextAsync(FilePath);
            return JsonSerializer.Deserialize<List<TodoItem>>(json) ?? new List<TodoItem>();
        }

        public async Task SaveTodosAsync(List<TodoItem> todos)
        {
            var json = JsonSerializer.Serialize(todos);
            await File.WriteAllTextAsync(FilePath, json);
        }
    }
}