
namespace Databalk.Application.Services;

public interface IDataTaskReadService
{
  Task<bool> ExistsByTitleAsync(string title);  
}
