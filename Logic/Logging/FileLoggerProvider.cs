using System.Text;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Logic.Logging;

/// <summary>
/// ファイルログプロバイダー
/// </summary>
[ProviderAlias("FileLogger")]
public sealed class FileLoggerProvider : ILoggerProvider, ISupportExternalScope
{
  /// <summary>
  /// ログ情報レコード
  /// </summary>
  public record struct LogEntry(DateTime TimeStamp, string Log);

  /// <summary>
  /// ファイルログ用設定クラスインスタンス
  /// </summary>
  private readonly FileLoggerOptions _options;

  /// <summary>
  /// スレッド間コミュケーションインスタンス
  /// </summary>
  private readonly Channel<LogEntry> _channel;

  /// <summary>
  /// 非同期処理用Task
  /// </summary>
  private readonly Task _work;

  /// <summary>
  /// ログコールスタック設定
  /// </summary>
  private IExternalScopeProvider _scopeProvider = new LoggerExternalScopeProvider();

  /// <summary>
  /// ファイルログ用設定クラスプロパティ
  /// </summary>
  public FileLoggerOptions Options => _options;

  /// <summary>
  /// スレッド間コミュケーションプロパティ
  /// </summary>
  public Channel<LogEntry> Channels => _channel;

  /// <summary>
  /// ログコールスタック設定プロパティ
  /// </summary>
  public IExternalScopeProvider ScopeProvider => _scopeProvider;

  /// <summary>
  /// コンストラクタ
  /// </summary>
  /// <param name="options">ファイルログ設定</param>
  public FileLoggerProvider(IOptions<FileLoggerOptions> options)
  {
    _options = options.Value;
    _channel = Channel.CreateBounded<LogEntry>(new BoundedChannelOptions(10_000)
    {
      SingleReader = true,
      FullMode = BoundedChannelFullMode.DropWrite
    });
    _work = Task.Run(ProcessQueueAsync);
  }

  /// <summary>
  /// ログ生成
  /// </summary>
  /// <param name="categoryName">カテゴリ</param>
  public ILogger CreateLogger(string categoryName) => new FileLogger(categoryName, this);

  /// <summary>
  /// スコープ設定
  /// </summary>
  /// <param name="scopeProvider">スコープ</param>
  public void SetScopeProvider(IExternalScopeProvider scopeProvider) => _scopeProvider = scopeProvider;

  /// <summary>
  /// 破棄処理
  /// </summary>
  public void Dispose()
  {
    _channel.Writer.TryComplete();
    _work.Wait(TimeSpan.FromSeconds(5));
  }

  /// <summary>
  /// ファイル書き込み
  /// </summary>
  /// <returns><非同期タスク/returns>
  public async Task ProcessQueueAsync()
  {
    StreamWriter? witer = null;
    DateOnly currentDate = default;
    var reader = _channel.Reader;

    try
    {
      while (await reader.WaitToReadAsync())
      {
        try
        {
          while (reader.TryRead(out var entry))
          {
            var date = DateOnly.FromDateTime(entry.TimeStamp);
            if (witer is not null || date != currentDate)
            {
              witer?.Dispose();
              witer = OpenWriter(date);
              currentDate = date;
              DeleteOldFiles();
            }
            witer?.WriteLine(entry.Log);
          }

          // キューが空になったらまた止めてフラッシュ
          witer?.Flush();
        }
        catch (Exception ex)
        {
          Console.Error.WriteLine($"File Error:{ex.Message}");
          witer?.Dispose();
          witer = null;
        }
      }
    }
    finally
    {
      witer?.Dispose();
    }
  }

  /// <summary>
  /// StreamWriterを設定して返す
  /// </summary>
  /// <param name="date">ログファイル名の年月日</param>
  /// <returns>StreamWriterインスタンス</returns>
  private StreamWriter OpenWriter(DateOnly date)
  {
    var baseDir = AppDomain.CurrentDomain.BaseDirectory;
    var dir = Path.IsPathRooted(_options.Directory) ? _options.Directory : Path.Combine(baseDir, _options.Directory);
    if (!Directory.Exists(dir))
      Directory.CreateDirectory(dir);

    var path = Path.Combine(dir, $"{_options.FileNamePrefix}_{date:yyyyMMdd}.log");
    var stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Write | FileShare.Delete);

    return new StreamWriter(stream, new UTF8Encoding(false));
  }

  /// <summary>
  /// 古い日付のファイルログを削除する
  /// </summary>
  private void DeleteOldFiles()
  {
    // 無制限の場合は終了
    if (_options.RetainedFileCount <= 0) return;

    try
    {
      // 削除対象の取得
      var baseDir = AppDomain.CurrentDomain.BaseDirectory;
      var dir = Path.IsPathRooted(_options.Directory) ? _options.Directory : Path.Combine(baseDir, _options.Directory);
      var oldFiles = Directory.GetFiles($"{dir}/{_options.FileNamePrefix}_*.log")
                        .OrderByDescending(fileName => fileName)
                        .Skip(_options.RetainedFileCount);

      // ファイルの削除
      foreach (var file in oldFiles)
      {
        File.Delete(file);
      }
    }
    catch
    {
      // noop
    }
  }
}
