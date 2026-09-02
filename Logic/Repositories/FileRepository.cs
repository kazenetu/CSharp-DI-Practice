using Logic.Interfaces.Repositories;

namespace Logic.Repositories;

/// <summary>
/// ファイルリポジトリ
/// </summary>
public class FileRepository : IFileRepository
{
    /// <summary>
    /// ファイル読み込み
    /// </summary>
    /// <param name="fileName">ファイル名</param>
    /// <returns>ファイルデータリスト</returns>
    public List<string> ReadFile(string fileName)
    {
        // ファイルパス生成
        var filePath = $"{AppDomain.CurrentDomain.BaseDirectory}/Resources/{fileName}";

        // ファイルパスが存在しない場合、リストゼロを返す
        if (!File.Exists(filePath))
            return [];

        // テキストファイル読み込み
        var fileText = File.ReadAllText(filePath);

        // 改行単位でリストを返す
        return [..fileText.Split(Environment.NewLine)];
    }
}