using Logic.Interfaces.Repositories;
using System.Text.Json;

namespace Logic.Repositories;

/// <summary>
/// JSONファイルリポジトリ
/// </summary>
internal class JsonFileRepository : IFileRepository
{
    /// <summary>
    /// ファイル読み込み
    /// </summary>
    /// <param name="fileName">ファイルのフルパス</param>
    /// <returns>ファイルデータリスト</returns>
    public List<string> ReadFile(string filePath)
    {
        // JSONファイル読み込み
        var text = File.ReadAllText(filePath);

        // JSONデコード
        var option = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var jsonModel = JsonSerializer.Deserialize<JsonModel>(text, option);

        // modelが存在しない場合は空リストを返す
        if (jsonModel is null) return [];

        // デコード結果を返す
        var result = jsonModel.Texts.Select(lineData => $"{lineData.Line}:{lineData.Text}");
        return [.. result];
    }

    /// <summary>
    /// JSON用モデル
    /// </summary>
    /// <param name="Texts">テキストリスト</param>
    private record JsonModel(List<LineData> Texts);

    /// <summary>
    /// 行要素
    /// </summary>
    /// <param name="Line">行番号</param>
    /// <param name="Text">文字列</param>
    /// <returns></returns>
    private record LineData(string Line, string Text);
}