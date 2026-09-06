using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Logic.Repositories;
using Logic.Interfaces.Repositories;
using Logic.Applictions;
using Logic.Interfaces.Applictions;

namespace ConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((Context, sevice) =>
            {
                // DI設定
                sevice.AddTransient<IFileRepository, FileRepository>();
                sevice.AddTransient<ISampleApplication, SampleApplication>();

                // 実行クラス
                sevice.AddSingleton<App>();
            })
            .Build();

        var app = host.Services.GetRequiredService<App>();
        app.Run();
    }
}
