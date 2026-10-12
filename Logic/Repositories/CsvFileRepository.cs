using Csv;
using Csv.Annotations;
using Logic.Interfaces.Repositories;

namespace Logic.Repositories;

/// <summary>
/// CSVファイルリポジトリ
/// </summary>
internal class CsvFileRepository : IFileRepository
{
    /// <summary>
    /// ファイル読み込み
    /// </summary>
    /// <param name="fileName">ファイルのフルパス</param>
    /// <returns>ファイルデータリスト</returns>
    public List<string> ReadFile(string filePath)
    {
        // CSVファイル読み込み
        using var reader = new FileStream(filePath, FileMode.Open);

        //CSVデシリアライズ
        var lines = CsvSerializer.Deserialize<CSVLineData>(reader);
        if (lines is null) return [];

        // デコード結果を返す
        var result = lines.Select(lineData => $"{lineData.No}:{lineData.Text}");
        return [.. result];
    }
}

/// <summary>
/// 行要素
/// </summary>
[CsvObject]
public partial class CSVLineData
{
    /// <summary>
    /// 行番号
    /// </summary>
    [Column("no")]
    public string? No { set; get; }

    /// <summary>
    /// 文字列
    /// </summary>
    [Column("text")]
    public string? Text { set; get; }
}
