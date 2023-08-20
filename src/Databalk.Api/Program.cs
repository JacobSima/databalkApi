using Databalk.Core;
using Databalk.Application;
using Databalk.Infrastructure;
using Databalk.Infrastructure.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services
  .AddCore()
  .AddApplication()
  .AddInfrastructure(builder.Configuration)
  .AddControllers();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();


app.Run();
