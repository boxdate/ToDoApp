using Microsoft.AspNetCore.Components;
using System.IO;
using System.Linq;
using System.Text.Json;
using ToDoApp.Data;

namespace ToDoApp.Components.Pages
{
    public partial class Todo : ComponentBase
    {
        private List<TodoItem> todos = new();
        private string? newTodoTitle;
        private DateOnly newTaskDate = DateOnly.FromDateTime(DateTime.Now);
        private const string FilePath = "todos.json";

        protected override async Task OnInitializedAsync() => await LoadTodosFromFile();

        private async Task LoadTodosFromFile()
        {
            if (File.Exists(FilePath))
            {
                var json = await File.ReadAllTextAsync(FilePath);
                todos = JsonSerializer.Deserialize<List<TodoItem>>(json) ?? new();
            }
        }

        private async Task SaveTodosToFile() => await File.WriteAllTextAsync(FilePath, JsonSerializer.Serialize(todos));

        private async Task AddTodo()
        {
            if (!string.IsNullOrWhiteSpace(newTodoTitle))
            {
                var newItem = new TodoItem { Title = newTodoTitle, IsDone = false, Date = newTaskDate };
                todos.Add(newItem);
                newTodoTitle = string.Empty;
                await SaveTodosToFile();
            }
        }
        
        // 子コンポーネントからの通知を受け取る
        private async Task RemoveTodo(TodoItem itemToRemove)
        {
            todos.Remove(itemToRemove);
            await SaveTodosToFile();
        }

        // 子コンポーネントからの通知を受け取る
        private async Task UpdateTodo(TodoItem itemToUpdate)
        {
            await SaveTodosToFile();
        }
    }
}