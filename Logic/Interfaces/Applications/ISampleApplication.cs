namespace Logic.Applictions;

/// <summary>
/// サンプルアプリケーション：インターフェイス
/// </summary>
public interface ISampleApplication
{
    /// <summary>
    /// データ取得
    /// </summary>
    /// <param name="fileName">ファイル名</param>
    /// <returns>ファイルデータリスト</returns>
    List<string> GetData(string fileName);
}