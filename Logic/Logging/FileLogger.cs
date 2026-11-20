using System.Text;
using Microsoft.Extensions.Logging;
using static Logic.Logging.FileLoggerProvider;

namespace Logic.Logging;

/// <summary>
/// ファイルログクラス
/// </summary>
/// <remarks>非スレッドセーフ</remarks>
public sealed class FileLogger(string category, FileLoggerProvider provider) : ILogger
{
  public IDisposable? BeginScope<TState>(TState state) where TState : notnull
  {
    return provider.ScopeProvider.Push(state);
  }

  /// <summary>
  /// ファイル出力可否判定
  /// </summary>
  /// <param name="logLevel">ロジック側のログレベル</param>
  /// <returns>true：ログ出力する / false：ログ出力しない</returns>
  public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;

  /// <summary>
  /// ログ出力
  /// </summary>
  /// <param name="logLevel">ログレベル</param>
  /// <param name="eventId">イベント<ID/param>
  /// <param name="state">例外発生ステート</param>
  /// <param name="exception">例外ログ</param>
  /// <param name="formatter">例外時のメッセージ作成処理</param>
  public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
  {
    if (!IsEnabled(logLevel)) return;

    var now = DateTime.Now;
    var sb = new StringBuilder();
    sb.Append($"{now:yyyyMMdd HH:mm:ss.fff} [{ToShort(logLevel)}] {category}");

    if (eventId != 0)
      sb.Append($"[{eventId}]");

    provider.ScopeProvider.ForEachScope((scope, sb) => sb.Append($" {scope}"), sb);
    sb.Append($" {formatter(state, exception)}");
    if (exception is not null)
      sb.AppendLine().Append(exception);

    provider.Channels.Writer.TryWrite(new LogEntry(now, sb.ToString()));
  }

  /// <summary>
  /// レベルの省略を取得する
  /// </summary>
  /// <param name="level">レベル</param>
  /// <returns>レベルの省略</returns>
  private static string ToShort(LogLevel level)
  {
    return level switch
    {
      LogLevel.Trace => "TRC",
      LogLevel.Debug => "DBG",
      LogLevel.Information => "INF",
      LogLevel.Warning => "WRN",
      LogLevel.Error => "ERR",
      LogLevel.Critical => "CRT",
      _ => "???"
    };
  }
}
