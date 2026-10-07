using System.Xml.Serialization;
using Logic.Interfaces.Repositories;

namespace Logic.Repositories;

/// <summary>
/// XMLファイルリポジトリ
/// </summary>
internal class XmlFileRepository : IFileRepository
{
    /// <summary>
    /// ファイル読み込み
    /// </summary>
    /// <param name="fileName">ファイルのフルパス</param>
    /// <returns>ファイルデータリスト</returns>
    public List<string> ReadFile(string filePath)
    {
        // XMLファイル読み込み
        using var reader = new FileStream(filePath, FileMode.Open);

        //XMLデシリアライズ
        var xmlSerializer = new XmlSerializer(typeof(XmlModel));
        var xmlModel = xmlSerializer.Deserialize(reader) as XmlModel;

        // modelが存在しない、Textsがnullの場合は空リストを返す
        if (xmlModel is null || xmlModel.Texts is null) return [];

        // デコード結果を返す
        return [.. xmlModel.Texts];
    }
}

/// <summary>
/// XML用モデル
/// </summary>
[XmlRoot("XMLModel")]
public class XmlModel
{
    [XmlElement("Text")]
    public List<string>? Texts { set; get; }
}
