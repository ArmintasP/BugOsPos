using BugOsPos.Api;
using BugOsPos.Api.Http;
using BugOsPos.Api.Mappings;
using BugOsPos.Application;
using BugOsPos.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration)
        .AddMappings()
        .AddSwagger();

    builder.Services.AddSingleton<ProblemDetailsFactory, BugOsProblemDetailsFactory>();
    builder.Services.AddControllers()
        .AddNewtonsoftJson(options =>
        {
            var dateConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter
            {
                DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ssZ"
            };

            options.SerializerSettings.Converters.Add(dateConverter);
            options.SerializerSettings.Culture = new CultureInfo("en-IE");
            options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
        });
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
