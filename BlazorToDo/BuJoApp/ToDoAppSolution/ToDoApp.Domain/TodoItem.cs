// namespace が ToDoApp.Domain であることを確認
namespace ToDoApp.Domain
{
    public class TodoItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Title { get; set; }
        public bool IsDone { get; set; }
        public DateOnly Date { get; set; }

        // ▼▼▼ この行が正しく存在することを確認 ▼▼▼
        public TaskPriority Priority { get; set; } = TaskPriority.Medium; 
    }
}