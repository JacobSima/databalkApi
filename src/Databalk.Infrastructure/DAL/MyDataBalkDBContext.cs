using Databalk.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Databalk.Infrastructure.DAL;

public sealed class MyDataBalkDBContext : DbContext
{
  public DbSet<DataTask> DataTasks {get; set;}

  public DbSet<User> Users {get; set;}

  public MyDataBalkDBContext(DbContextOptions<MyDataBalkDBContext> options) : base(options) {}

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
  }
}
