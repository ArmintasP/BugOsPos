using BugOsPos.Api.Http;
using BugOsPos.Api.Mappings;
using BugOsPos.Application;
using BugOsPos.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration)
        .AddMappings();

    builder.Services.AddSingleton<ProblemDetailsFactory, BugOsProblemDetailsFactory>();
    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();
    app.MapControllers();
}

app.Run();
