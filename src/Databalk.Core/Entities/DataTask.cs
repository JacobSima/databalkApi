using Databalk.Core.ValueObjects;

namespace Databalk.Core.Entities;

public class DataTask
{
  public DataTaskId Id {get; private set;}

  public Title Title {get; private set;}

  public Description Description {get; private set;}

  public Date DueDate {get; private set;}

  public UserId AssigneeId {get; private set;}

  public User Assignee {get; set;}

  public DataTask(){}

  public DataTask(DataTaskId id, Title title, Description description, Date dueDate, UserId assignee)
  {
    Id = id;
    Title = title;
    Description =  description;
    DueDate = dueDate;
    AssigneeId = assignee;
  }

  public void Update(string title, string description, DateTime dueDate, Guid assignee)
  {
    Title = title;
    Description = description;
    DueDate = dueDate;
    AssigneeId = assignee;
  }
}
