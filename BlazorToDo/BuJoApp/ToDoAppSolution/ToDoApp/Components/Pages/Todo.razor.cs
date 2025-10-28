using Microsoft.AspNetCore.Components;
using ToDoApp.Application;
using ToDoApp.Domain;

namespace ToDoApp.Components.Pages
{
    public partial class Todo : ComponentBase
    {
        [Inject] // ← DIコンテナからサービスを受け取る
        private ITodoService TodoService { get; set; } = default!;

        private List<TodoItem> todos = new();
        private string? newTodoTitle;
        private DateOnly newTaskDate = DateOnly.FromDateTime(DateTime.Now);

        protected override async Task OnInitializedAsync()
        {
            // サービスを使ってデータを読み込む
            todos = await TodoService.GetTodosAsync();
        }

        private async Task AddTodo()
        {
            if (!string.IsNullOrWhiteSpace(newTodoTitle))
            {
                var newItem = new TodoItem { Title = newTodoTitle, IsDone = false, Date = newTaskDate };
                todos.Add(newItem);
                newTodoTitle = string.Empty;
                
                // サービスを使ってデータを保存する
                await TodoService.SaveTodosAsync(todos);
            }
        }
        
        private async Task RemoveTodo(TodoItem itemToRemove)
        {
            todos.Remove(itemToRemove);
            await TodoService.SaveTodosAsync(todos);
        }

        private async Task UpdateTodo(TodoItem itemToUpdate)
        {
            await TodoService.SaveTodosAsync(todos);
        }
    }
}