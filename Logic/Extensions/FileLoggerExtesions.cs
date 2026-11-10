using Logic.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace Logic.Extensions;

/// <summary>
/// ファイルログ用拡張クラス
/// </summary>
public static class FileLoggerExtensions
{
  /// <summary>
  /// ファイルログ設定
  /// </summary>
  /// <param name="builder">ログプロバイダー</param>
  /// <returns>ファイルログ設定済みログプロバイダー</returns>
  public static ILoggingBuilder AddFileLoader(this ILoggingBuilder builder, Action<FileLoggerOptions>? config = null)
  {
    builder.AddConfiguration();

    builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, FileLoggerProvider>());
    LoggerProviderOptions.RegisterProviderOptions<FileLoggerOptions, FileLoggerProvider>(builder.Services);

    if (config is not null)
      builder.Services.Configure(config);

    return builder;
  }
}
