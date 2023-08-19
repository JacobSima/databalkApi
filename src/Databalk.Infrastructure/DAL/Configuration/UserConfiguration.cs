
using Databalk.Core.ValueObjects;
using Databalk.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Databalk.Infrastructure.DAL.Configuration;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id)
        .HasConversion(x => x.Value, x => new UserId(x));

    builder.HasIndex(x => x.Email).IsUnique();
    builder.Property(x => x.Email)
        .HasConversion(x => x.Value, x => new Email(x))
        .IsRequired()
        .HasMaxLength(100);
    
    builder.HasIndex(x => x.Username).IsUnique();
    builder.Property(x => x.Username)
        .HasConversion(x => x.Value, x => new Username(x))
        .IsRequired()
        .HasMaxLength(30);
    
    builder.Property(x => x.Password)
        .HasConversion(x => x.Value, x => new Password(x))
        .IsRequired()
        .HasMaxLength(200);
  }
}
