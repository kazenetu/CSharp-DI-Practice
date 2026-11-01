using Logic.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
        var builder = Host.CreateApplicationBuilder(args);
        builder.Environment.ContentRootPath = AppContext.BaseDirectory;

        // ロジック部のDI登録と実行クラス登録
        builder.Services.AddLogicDI().AddTransient<App>();

        //IHost取得
        var host = builder.Build();

        // 実行クラスのインスタンス生成・実行
        var app = host.Services.GetRequiredService<App>();
        app.Run();
    }
}
