using Microsoft.EntityFrameworkCore;
using Databalk.Infrastructure.DAL;

namespace Databalk.Tests.Unit;

internal sealed class TestDatabase : IDisposable
{
  public MyDataBalkDBContext Context {get;}

  public TestDatabase()
  {
    var options = new OptionsProvider().Get<PostgresOptions>("postgres");
    Context = new MyDataBalkDBContext(new DbContextOptionsBuilder<MyDataBalkDBContext>().UseNpgsql(options.ConnectionString).Options);
  }
  public void Dispose()
  {
    Context.Database.EnsureDeleted();
    Context.Dispose();
  }
}
