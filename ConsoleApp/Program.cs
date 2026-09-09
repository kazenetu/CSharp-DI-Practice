using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Logic.Repositories;
using Logic.Interfaces.Repositories;
using Logic.Applictions;
using Logic.Interfaces.Applictions;

namespace ConsoleApp;

/// <summary>
/// エントリポイント
/// </summary>
class Program
{
    /// <summary>
    /// メインメソッド
    /// </summary>
    /// <param name="args">コマンドライン引数</param>
    static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((Context, sevice) =>
            {
                // ロジック部のDI登録
                sevice.AddTransient<IFileRepository, FileRepository>();
                sevice.AddTransient<ISampleApplication, SampleApplication>();

                // 実行クラス登録
                sevice.AddTransient<App>();
            })
            .Build();

        // 実行クラスのインスタンス生成・実行
        var app = host.Services.GetRequiredService<App>();
        app.Run();
    }
}
