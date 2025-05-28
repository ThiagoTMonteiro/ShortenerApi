using Microsoft.EntityFrameworkCore;
using ShortenerApi.Models;

namespace ShortenerApi.Middlewares;

public class MetricsMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, AppDbContext db)
    {
        var path = context.Request.Path.Value?.Trim('/');

        if (!string.IsNullOrEmpty(path) && !path.StartsWith("api"))
        {
            var link = await db.Links.FirstOrDefaultAsync(x => x.ShortCode == path);
            if (link != null)
            {
                db.LinkMetrics.Add(new LinkMetric
                {
                    IpAddress = context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                    UserAgent = context.Request.Headers.UserAgent.ToString(),
                    LinkId = link.Id,
                    AccessedAt =  DateTime.UtcNow
                });
                link.Clicks++;
                await db.SaveChangesAsync();
            }
        }
        await next(context);  
    }
}