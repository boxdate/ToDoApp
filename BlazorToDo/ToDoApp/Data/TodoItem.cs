namespace ToDoApp.Data
{
    public class TodoItem
    {
        // どのタスクかを一意に識別するためのID
        public Guid Id { get; set; } = Guid.NewGuid();
        
        public string? Title { get; set; }
        
        // タスクの種類（タスク・イベント・メモ）を保存
        public TaskType Type { get; set; }
        
        // タスクの状態（未完了・完了など）を保存
        public TaskStatus Status { get; set; }
        
        // IsDoneは不要になるので削除
        // public bool IsDone { get; set; }
    }
}
