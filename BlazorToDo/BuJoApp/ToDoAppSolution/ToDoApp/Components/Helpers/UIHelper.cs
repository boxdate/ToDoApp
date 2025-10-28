using ToDoApp.Domain; // TaskPriorityを使うため

namespace ToDoApp.Components.Helpers
{
    public static class UIHelper // staticクラスにするのが一般的
    {
        public static string GetPriorityBadgeClass(TaskPriority priority)
        {
            return priority switch
            {
                TaskPriority.High => "bg-danger",
                TaskPriority.Medium => "bg-warning text-dark",
                TaskPriority.Low => "bg-secondary",
                _ => "bg-light text-dark" // 念のためデフォルト値
            };
        }
    }
}