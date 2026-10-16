using Logic.Interfaces.Repositories;

namespace Logic.Repositories;

/// <summary>
/// テキストファイルリポジトリ
/// </summary>
internal class TextFileRepository : IFileRepository
{
    /// <summary>
    /// ファイル読み込み
    /// </summary>
    /// <param name="fileName">ファイルのフルパス</param>
    /// <returns>ファイルデータリスト</returns>
    public List<string> ReadFile(string filePath)
    {
        // テキストファイル読み込み
        var fileText = File.ReadAllText(filePath);

        // 改行単位でリストを返す
        return [.. fileText.Split(Environment.NewLine)];
    }
}