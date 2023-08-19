
using Databalk.Application.DTO;
using Databalk.Core.Entities;

namespace Databalk.Infrastructure.DAL.Queries.Handlers;

internal static class Extentions
{
  public static DataTaskDto AsDto(this DataTask dataTask) 
    => new() 
    {
      Id = dataTask.Id,
      Title = dataTask.Title,
      Description = dataTask.Description,
      DueDate = dataTask.DueDate
    };
}
