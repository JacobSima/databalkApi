
using Databalk.Core.ValueObjects;

namespace Databalk.Core.Entities;

public class User
{
  public User(UserId id, Email email, Username username, Password password)
  {
    Id = id;
    Email = email;
    Username = username;
    Password = password;
  }

  public UserId Id {get;} 

   public Email Email {get;}

   public Username Username {get;}

   public Password Password {get;}

   public ICollection<DataTask> DataTasks {get; set;}
}
