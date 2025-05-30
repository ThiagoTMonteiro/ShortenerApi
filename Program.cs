using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using ShortenerApi;
using ShortenerApi.Middlewares;
using ShortenerApi.Repositories;
using ShortenerApi.Repositories.Interfaces;
using ShortenerApi.Services;
using ShortenerApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ILinkService, LinkService>();
builder.Services.AddScoped<IShortCodeGeneratorService, ShortCodeGeneratorService>();
builder.Services.AddScoped<IAppSettingService, AppSettingService>();
builder.Services.AddScoped<ILinkRepository, LinkRepository>();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseMiddleware<MetricsMiddleware>();

app.MapControllers();

app.MapFallback(async (context) =>
{
    var shortCode = context.Request.Path.Value?.Trim('/');
    if (!string.IsNullOrEmpty(shortCode))
    {
        var service = context.RequestServices.GetRequiredService<ILinkService>();
        var url = await service.ResolveShortCodeAsync(shortCode);
        if (!string.IsNullOrEmpty(url))
        {
            context.Response.Redirect(url, false);
            return;
        }
    }
    context.Response.StatusCode = StatusCodes.Status404NotFound;
});

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
db.Database.Migrate();

app.Run();


