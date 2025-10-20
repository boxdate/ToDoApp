namespace ToDoApp.Data
{
    public class TodoItem
    {
        public string? Title { get; set; }
        public bool IsDone { get; set; }
        
        // この行が正しく存在することを確認してください
        public DateOnly Date { get; set; }
    }
}