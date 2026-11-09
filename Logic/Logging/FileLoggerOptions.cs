namespace Logic.Logging;

/// <summary>
/// ファイルログ用設定クラス
/// </summary>
public class FileLoggerOptions
{
   /// <summary>
   /// 出力先
   /// </summary>
   public string Directory { get; set; } = "Logs";

   /// <summary>
   /// ファイル名のプレフィックス
   /// </summary>
   public string FileNamePrefix { get; set; } = "app";

   /// <summary>
   /// 保持するファイル数（0以下で無制限）
   /// </summary>
   public int RetainedFileCount { get; set; } = 7;
}
