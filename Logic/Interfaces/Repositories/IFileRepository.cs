namespace Logic.Interfaces.Repositories;

/// <summary>
/// ファイルリポジトリ：インターフェイス
/// </summary>
public interface IFileRepository
{
    /// <summary>
    /// ファイル読み込み
    /// </summary>
    /// <param name="fileName">ファイル名</param>
    /// <returns>ファイルデータリスト</returns>
    List<string> ReadFile(string fileName);
}