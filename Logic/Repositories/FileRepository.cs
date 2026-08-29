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
        // ダミーデータを返す
        return ["A", "B", "C"];
    }
}