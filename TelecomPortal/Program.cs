using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TelecomPortal.Common.Constants;
using TelecomPortal.Data.Extensions;
using TelecomPortal.Data.Repository.Context;
using TelecomPortal.Data.Repository.Seeder;
using TelecomPortal.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TelecomPortalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(TelecomConstants.TelecomLocalDB)));

builder.Services.AddControllers();

builder.Services.AddServices();
builder.Services.AddRepositories();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString(TelecomConstants.TelecomLocalDB)!, name: "SQL Server");

builder.Services.AddTransient<CustomerAccountSeeder>();

builder.Services.AddHealthChecksUI(options =>
{
    options.SetEvaluationTimeInSeconds(15);
    options.MaximumHistoryEntriesPerEndpoint(60);
    options.AddHealthCheckEndpoint("API Health", "/healthz"); 
}).AddInMemoryStorage();

builder.Services.AddCors(options =>
{
    options.AddPolicy("TelecomPortal.Client", builder =>
    {
        builder.WithOrigins("http://localhost:5173")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<CustomerAccountSeeder>();
    await seeder.SeedAsync();
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    options.RoutePrefix = string.Empty;
});

app.UseExceptionHandler(config =>
{
    config.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var exception = context.Features.Get<IExceptionHandlerFeature>();
        if (exception != null)
        {
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError(exception.Error, "Unhandled exception occurred.");

            var response = new
            {
                error = "An unexpected error occurred.",
                details = exception.Error.Message
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    });
});

app.UseHealthChecks("/healthz", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHealthChecksUI(config =>
{
    config.UIPath = "/health-ui";
});

app.UseHttpsRedirection();

app.UseCors("TelecomPortal.Client");

app.UseAuthorization();

app.MapControllers();

app.Run();
