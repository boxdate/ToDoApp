using Microsoft.AspNetCore.Components;
using System.IO;
using System.Linq;
using System.Text.Json;
using ToDoApp.Data;

// ▼▼▼ この namespace を「.Pages」が付いていないものに修正！ これが最終修正です。 ▼▼▼
namespace ToDoApp.Components
{
    public partial class Todo : ComponentBase
    {
        private List<TodoItem> todos = new();
        private string? newTodoTitle;
        private TaskType newTaskType = TaskType.Task;
        private const string FilePath = "todos.json";

        protected override async Task OnInitializedAsync()
        {
            await LoadTodosFromFile();
        }

        private async Task LoadTodosFromFile()
        {
            if (File.Exists(FilePath))
            {
                var json = await File.ReadAllTextAsync(FilePath);
                var loadedTodos = JsonSerializer.Deserialize<List<TodoItem>>(json);
                if (loadedTodos is not null)
                {
                    todos = loadedTodos;
                }
            }
        }

        private async Task SaveTodosToFile()
        {
            var json = JsonSerializer.Serialize(todos);
            await File.WriteAllTextAsync(FilePath, json);
        }

        private async Task AddTodo()
        {
            if (!string.IsNullOrWhiteSpace(newTodoTitle))
            {
                var newItem = new TodoItem
                {
                    Title = newTodoTitle,
                    Type = newTaskType,
                    Status = ToDoApp.Data.TaskStatus.Incomplete
                };
                
                todos.Add(newItem);
                todos = new List<TodoItem>(todos); 

                newTodoTitle = string.Empty;
                await SaveTodosToFile();
            }
        }

        private async Task UpdateTaskStatus(Guid id, ToDoApp.Data.TaskStatus newStatus)
        {
            todos = todos.Select(t =>
            {
                if (t.Id == id) t.Status = newStatus;
                return t;
            }).ToList();
            await SaveTodosToFile();
        }

        private async Task RemoveTodo(Guid id)
        {
            todos = todos.Where(t => t.Id != id).ToList();
            await SaveTodosToFile();
        }
    }
}