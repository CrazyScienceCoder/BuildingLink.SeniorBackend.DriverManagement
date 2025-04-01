using BuildingLink.DriverManagement.Application.Extensions;
using BuildingLink.DriverManagement.Infrastructure.Extensions;
using BuildingLink.DriverManagement.Infrastructure.Migrator.Extensions;
using BuildingLink.DriverManagement.WebApi;
using BuildingLink.DriverManagement.WebApi.Extensions;
using BuildingLink.DriverManagement.WebApi.Filters;
using BuildingLink.DriverManagement.WebApi.Middlewares;
using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSerilog();

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddMigrator();

var app = builder.Build();

app.RunDatabaseMigrator();

app.UseSerilogRequestLogging();
app.UseMiddleware<RequestLoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
