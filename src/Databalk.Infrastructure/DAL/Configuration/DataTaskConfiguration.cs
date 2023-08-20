
using Databalk.Core.ValueObjects;
using Databalk.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Databalk.Infrastructure.DAL.Configuration;

public class DataTaskConfiguration : IEntityTypeConfiguration<DataTask>
{
  public void Configure(EntityTypeBuilder<DataTask> builder)
  {
    // For DataTaskId
    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id)
      .HasConversion(x => x.Value, x => new DataTaskId(x));
    
    builder.Property(x => x.Title)
      .IsRequired()
      .HasConversion(x => x.Value, x => new Title(x));

    builder.Property(x => x.Description)
      .HasConversion(x => x.Value, x => new Description(x));
    
    builder.Property(x => x.DueDate)
      .IsRequired()
      .HasConversion(x => x.Value, x => new Date(x));
    
    builder.Property(x => x.Assignee)
      .IsRequired()
      .HasConversion(x => x.Value, x => new UserId(x));
  }
}
