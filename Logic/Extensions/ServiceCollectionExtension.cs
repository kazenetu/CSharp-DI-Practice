using Microsoft.Extensions.DependencyInjection;

namespace Logic.Extensions;

/// <summary>
/// IServiceCollection拡張クラス
/// </summary>
public static class ServiceCollectionExtension
{
   /// <summary>
   /// DIコンテナ設定：ロジック部の登録
   /// </summary>
   /// <param name="service">IServiceCollectionインスタンス</param>
   /// <returns>IServiceCollectionインスタンsy</returns>
   public static IServiceCollection AddLogicDI(this IServiceCollection service)
   {
      return service;
   }
}
