using Databalk.Core.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Databalk.Core;

public static class Extentions
{
   public static IServiceCollection AddCore(this IServiceCollection services)
   {
      services.AddSingleton<IDataTaskFactory, DataTaskFactory>();
      return services;
   } 
}
