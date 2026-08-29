using Logic.Interfaces.Repositories;

namespace Logic.Applictions;

/// <summary>
/// サンプルアプリケーション：インターフェイス
/// </summary>
public class SampleApplication: ISampleApplication
{
    /// <summary>
    /// ファイルリポジトリ
    /// </summary>
    private IFileRepository _fileRepository;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="fileRepository">ファイルリポジトリ</param>
    public SampleApplication(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    /// <summary>
    /// データ取得
    /// </summary>
    /// <param name="fileName">ファイル名</param>
    /// <returns>ファイルデータリスト</returns>
    public List<string> GetData(string fileName)
    {
        return _fileRepository.ReadFile(fileName);
    }
}