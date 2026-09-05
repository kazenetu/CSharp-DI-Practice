using Logic.Interfaces.Applictions;

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
        var textList = application.GetData("dummy.txt");
        Console.WriteLine(string.Join(Environment.NewLine, textList));
    }
}