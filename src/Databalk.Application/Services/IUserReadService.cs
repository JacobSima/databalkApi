
namespace Databalk.Application.Services;

public interface IUserReadService
{
  Task<bool> ExistsByEmailAsync(string email);  
  Task<bool> ExistsByUsernameAsync(string username);   
}
