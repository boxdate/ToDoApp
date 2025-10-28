using System.IO;
using System.Text.Json;
using ToDoApp.Application;
using ToDoApp.Domain;

namespace ToDoApp.Infrastructure
{
    public class JsonTodoService : ITodoService
    {
        // ファイルパスを保存する
        private readonly string _filePath;

        // ファイルパスを受け取るコンストラクタ
        public JsonTodoService(string filePath = "todos.json") // デフォルトは "todos.json"
        {
            _filePath = filePath;
        }

        public async Task<List<TodoItem>> GetTodosAsync()
        {
            // 保存されたファイルパスを使用する
            if (!File.Exists(_filePath))
            {
                return new List<TodoItem>();
            }
            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<TodoItem>>(json) ?? new List<TodoItem>();
        }

        public async Task SaveTodosAsync(List<TodoItem> todos)
        {
            var json = JsonSerializer.Serialize(todos);
            // 保存されたファイルパスを使用する
            await File.WriteAllTextAsync(_filePath, json);
        }
    }
}