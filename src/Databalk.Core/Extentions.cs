using System.Runtime.CompilerServices;
using Databalk.Core.Factories;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Databalk.Tests.Unit")]
namespace Databalk.Core;

public static class Extentions
{
   public static IServiceCollection AddCore(this IServiceCollection services)
   {
      services.AddSingleton<IDataTaskFactory, DataTaskFactory>();
      services.AddSingleton<IUserFactory, UserFactory>();
      
      return services;
   } 
}
