using Logic.Interfaces.Applications;

namespace ConsoleApp;

/// <summary>
/// 実行クラス
/// </summary>
class App(ISampleApplication application)
{
    /// <summary>
    /// 実行
    /// </summary>
    public void Run()
    {
        // テキストファイル
        Console.WriteLine("---TextFile---");
        var textList = application.GetData("dummy.txt");
        Console.WriteLine(string.Join(Environment.NewLine, textList));
        Console.WriteLine();

        // JSONファイル
        Console.WriteLine("---JSONFile---");
        var jsonList = application.GetData("dummy.json");
        Console.WriteLine(string.Join(Environment.NewLine, jsonList));
        Console.WriteLine();

        // XMLファイル
        Console.WriteLine("---XMLFile---");
        var xmlList = application.GetData("dummy.xml");
        Console.WriteLine(string.Join(Environment.NewLine, xmlList));
        Console.WriteLine();
    }
}