using System.Text;
using Logic.Interfaces.Applications;
using Microsoft.Extensions.Logging;

namespace ConsoleApp;

/// <summary>
/// 実行クラス
/// </summary>
/// <param name="logger">ログ</param>
/// <param name="application">ファイル取得アプリケーション</param>
class App(ILogger<App> logger, ISampleApplication application)
{
    /// <summary>
    /// 実行
    /// </summary>
    public void Run()
    {
        var sb = new StringBuilder();

        // テキストファイル
        sb.AppendLine("---TextFile---");
        var textList = application.GetData("dummy.txt");
        sb.AppendLine(string.Join(Environment.NewLine, textList));
        logger.LogInformation(sb.ToString());

        // JSONファイル
        sb.Clear();
        sb.AppendLine("---JSONFile---");
        var jsonList = application.GetData("dummy.json");
        sb.AppendLine(string.Join(Environment.NewLine, jsonList));
        logger.LogInformation(sb.ToString());

        // XMLファイル
        sb.Clear();
        sb.AppendLine("---XMLFile---");
        var xmlList = application.GetData("dummy.xml");
        sb.AppendLine(string.Join(Environment.NewLine, xmlList));
        logger.LogInformation(sb.ToString());

        // CSVファイル
        sb.Clear();
        sb.AppendLine("---CSVFile---");
        var csvlList = application.GetData("dummy.csv");
        sb.AppendLine(string.Join(Environment.NewLine, csvlList));
        logger.LogInformation(sb.ToString());
    }
}