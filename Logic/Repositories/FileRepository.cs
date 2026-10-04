using Logic.Interfaces.Repositories;

namespace Logic.Repositories;

/// <summary>
/// ファイルリポジトリ
/// </summary>
internal class FileRepository : IFileRepository
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

        // 拡張子取得
        var fileExt = Path.GetExtension(fileName);
        IFileRepository targetRepository = fileExt.ToLower() switch
        {
            ".txt" => new TextFileRepository(),
            ".json" => new JsonFileRepository(),
            _ => throw new Exception($"拡張子[{fileExt}]に紐づく処理が見つかりません。")
        };

        // 改行単位でリストを返す
        return targetRepository.ReadFile(filePath);
    }
}