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

        // タスクの優先度を保持する変数を追加
       private TaskPriority newTaskPriority = TaskPriority.Medium; // デフォルトはミディアム

        protected override async Task OnInitializedAsync() => await LoadTodos();

        private async Task LoadTodos() => todos = await TodoService.GetTodosAsync();
        private async Task SaveTodos() => await TodoService.SaveTodosAsync(todos);


        private async Task AddTodo()
        {
            if (!string.IsNullOrWhiteSpace(newTodoTitle))
            {
                var newItem = new TodoItem
                {
                    Title = newTodoTitle,
                    IsDone = false,
                    Date = newTaskDate,
                    Priority =newTaskPriority // 選択された優先度を保存
                };
                todos.Add(newItem);
                todos = new List<TodoItem>(todos);
                newTodoTitle = string.Empty;
                newTaskPriority = TaskPriority.Medium; // 入力後、優先度はデフォルトに戻す
                
                // サービスを使ってデータを保存する
                await SaveTodos();
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