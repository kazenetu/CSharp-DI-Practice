using Logic.Interfaces.Applications;
using Microsoft.Extensions.Logging;

namespace ConsoleApp;

/// <summary>
/// 実行クラス
/// </summary>
class App(ILogger<App> logger, ISampleApplication application)
{
    /// <summary>
    /// 実行
    /// </summary>
    public void Run()
    {
        // テキストファイル
        logger.LogInformation("---TextFile---");
        var textList = application.GetData("dummy.txt");
        var textResult = string.Join(Environment.NewLine, textList);
        logger.LogInformation(textResult);

        // JSONファイル
        logger.LogInformation("---JSONFile---");
        var jsonList = application.GetData("dummy.json");
        var jsonResult = string.Join(Environment.NewLine, jsonList);
        logger.LogInformation(jsonResult);

        // XMLファイル
        logger.LogInformation("---XMLFile---");
        var xmlList = application.GetData("dummy.xml");
        var xmlResult = string.Join(Environment.NewLine, xmlList);
        logger.LogInformation(xmlResult);

        // CSVファイル
        logger.LogInformation("---CSVFile---");
        var csvlList = application.GetData("dummy.csv");
        var csvResult = string.Join(Environment.NewLine, csvlList);
        logger.LogInformation(csvResult);
    }
}