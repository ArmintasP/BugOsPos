using BugOsPos.Api;
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
        .AddMappings()
        .AddSwagger();

    builder.Services.AddSingleton<ProblemDetailsFactory, BugOsProblemDetailsFactory>();
    builder.Services.AddControllers();

}

var app = builder.Build();
{
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();
    app.MapControllers();
}

app.Run();
