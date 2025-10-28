using ToDoApp.Domain; // TaskPriorityを使うため
using ToDoApp.Components.Helpers; // これから作るUIHelperクラスのため
using Xunit;

namespace ToDoApp.Tests;

public class UIHelperTests
{
    [Theory] // 複数のデータで同じテストを実行
    [InlineData(TaskPriority.High, "bg-danger")]
    [InlineData(TaskPriority.Medium, "bg-warning text-dark")]
    [InlineData(TaskPriority.Low, "bg-secondary")]
    public void GetPriorityBadgeClass_ReturnsCorrectClass(TaskPriority priority, string expectedClass)
    {
        // Act (実行)
        var actualClass = UIHelper.GetPriorityBadgeClass(priority); // まだ存在しないメソッドを呼ぶ

        // Assert (検証)
        Assert.Equal(expectedClass, actualClass);
    }
}