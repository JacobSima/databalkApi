using Databalk.Core.ValueObjects;
using Microsoft.VisualBasic;

namespace Databalk.Core.Entities;

public class DataTask
{
  public DataTaskId Id {get;}

  public Title Title {get;}

  public Description Description {get;}

  public Date DueDate {get;}

  public DataTask(DataTaskId id, Title title, Description description, Date dueDate)
  {
    Id = id;
    Title = title;
    Description =  description;
    DueDate = dueDate;
  }
}
