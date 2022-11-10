using CarDealership.BL.CommandHandlers.CarCommandHandlers;
using CarDealership.Extensions;
using CarDealership.HealthChecks;
using CarDealership.Models.Configurations;
using MediatR;
using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using CarDealership.Middleware;

//Config
var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
        .Build();

//Serilog
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration, sectionName: "Serilog")
    .Enrich.FromLogContext()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

//Serilog
builder.Logging.AddSerilog(logger);

//Configs
builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection(nameof(KafkaSettings)));

//Add services to the container.
builder.Services.RegisterRepositories();
builder.Services.RegisterServices();
builder.Services.AddAutoMapper(typeof(Program));

//FluentValidation
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//HealthChecks
builder.Services.AddHealthChecks()
    .AddCheck<SQLHealthCheck>("SQL Server");

//MediatR
builder.Services.AddMediatR(typeof(CreateCarCommandHandler).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.RegisterHealthChecks();

app.MapHealthChecks("/CheckAppHealth");

app.UseMiddleware<ErrorHandlerMiddleware>();

app.Run();
