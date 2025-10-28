using ToDoApp.Domain;
using ToDoApp.Infrastructure; // このusingを追加
using System.Text.Json; // このusingを追加
using Xunit; // xUnitが参照されていることを確認

namespace ToDoApp.Tests; // フォルダと一致する名前空間

public class JsonTodoServiceTests
{
    // 各テスト用に一意の一時ファイルパスを作成するヘルパーメソッド
    private string GetTempFilePath() => Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");

    [Fact] // この属性はメソッドがテストであることを示します
    public async Task SaveAndLoad_ShouldPreserveData()
    {
        // Arrange (準備 - セットアップ)
        var filePath = GetTempFilePath();
        var service = new JsonTodoService(filePath); // 一時ファイルパスを使用
        var originalTodos = new List<TodoItem>
        {
            new TodoItem { Title = "Test Task 1", IsDone = false, Date = DateOnly.FromDateTime(DateTime.Now) }
        };

        try // try/finallyを使用してクリーンアップを保証
        {
            // Act (実行 - アクションを実行)
            await service.SaveTodosAsync(originalTodos);
            var loadedTodos = await service.GetTodosAsync();

            // Assert (検証 - 結果を確認)
            Assert.NotNull(loadedTodos); // nullでないことを確認
            Assert.Single(loadedTodos); // 項目がちょうど1つであることを確認
            Assert.Equal("Test Task 1", loadedTodos[0].Title); // タイトルを確認
            Assert.False(loadedTodos[0].IsDone); // 状態を確認
        }
        finally
        {
            // Cleanup (後片付け - 一時ファイルを削除)
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }

    [Fact]
    public async Task Load_NonExistentFile_ShouldReturnEmptyList()
    {
        // Arrange
        var filePath = GetTempFilePath(); // まだ存在しないファイル
        var service = new JsonTodoService(filePath);

        // Act
        var loadedTodos = await service.GetTodosAsync();

        // Assert
        Assert.NotNull(loadedTodos);
        Assert.Empty(loadedTodos); // 空のリストであるべき
    }
}