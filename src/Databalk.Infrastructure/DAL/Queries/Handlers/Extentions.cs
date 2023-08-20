
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
    DueDate = dataTask.DueDate,
    Assignee = dataTask.Assignee.AsDto()
  };

  public static UserDto AsDto(this User user)
   => new()
   {
    Id = user.Id,
    Email = user.Email,
    Username = user.Username
   };
}
