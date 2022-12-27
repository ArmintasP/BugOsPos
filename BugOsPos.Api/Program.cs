using BugOsPos.Api.Http;
using BugOsPos.Api.Mappings;
using BugOsPos.Application;
using BugOsPos.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration)
        .AddMappings();

    builder.Services.AddSingleton<ProblemDetailsFactory, BugOsProblemDetailsFactory>();
    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        var xmlDocumentationFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlDocumentationFile));

        options.DescribeAllParametersInCamelCase();

        options.MapType<TimeSpan>(() => new OpenApiSchema
        {
            Type = "string",
            Example = new OpenApiString("00:00:00")
        });
    });
}

var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();
    app.MapControllers();
}

app.Run();
