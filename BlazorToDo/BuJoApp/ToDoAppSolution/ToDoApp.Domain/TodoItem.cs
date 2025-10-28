// namespace が ToDoApp.Domain であることを確認
namespace ToDoApp.Domain
{
    public class TodoItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Title { get; set; }
        // public TaskType Type { get; set; } // バレットジャーナル用。もし残っていたら削除
        // public TaskStatus Status { get; set; } // バレットジャーナル用。もし残っていたら削除
        public bool IsDone { get; set; } // シンプルなToDoに戻した場合
        public DateOnly Date { get; set; }
    }
}