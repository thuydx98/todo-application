using Azure.Identity;
using Dekra.Todo.Api.Data.Contracts.EntityFramework;
using Dekra.Todo.Api.Data.Entities;
using Dekra.Todo.Api.Data.EntityFramework;
using Dekra.Todo.Api.Infrastructure.Config.Authentication;
using Dekra.Todo.Api.Infrastructure.Config.Authentication.Object;
using Dekra.Todo.Api.Infrastructure.Config.Middlewares;
using Dekra.Todo.Api.Infrastructure.Utilities.Converters;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var dbConnection = builder.Configuration[SettingConst.ConnectionStringsConst.Database];

if (!builder.Environment.IsEnvironment("Local"))
{
    builder.Configuration.AddAzureKeyVault(new Uri($"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/"), new DefaultAzureCredential());
}

builder.Services.AddDbContext<DekraDbContext>(options => options.UseSqlServer(dbConnection));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork<DekraDbContext>>();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.Converters.Add(new JsonStringDateTimeConverter());
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddAuthenticationConfig(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy(ConfigConst.CorsPolicy, policy => policy
        .WithOrigins(builder.Configuration.GetSection(SettingConst.CorsConst.AllowOrigins).Get<string[]>()!)
        .AllowAnyMethod()
        .AllowAnyHeader());
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors(ConfigConst.CorsPolicy);
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication().UseAuthorization();
app.MapControllers().RequireAuthorization();
app.Run();
