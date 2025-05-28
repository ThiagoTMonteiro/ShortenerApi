using Microsoft.EntityFrameworkCore;
using ShortenerApi.Models;
using ShortenerApi.Models.DTOs;
using ShortenerApi.Services.Interfaces;

namespace ShortenerApi.Services;

public class LinkService(AppDbContext context, IConfiguration config) : ILinkService
{
    public async Task<string> CreateShortLinkTask(string originalUrl)
    {
        var shortCode = Guid.NewGuid().ToString()[..6];
        var entry = new Link{ OriginalUrl = originalUrl, ShortCode = shortCode };
        context.Links.Add(entry);
        await context.SaveChangesAsync();
        
        var domain = config["AppSettings:Domain"] ?? "http://localhost";
        return $"{domain}/{shortCode}";
    }

    public async Task<string?> ResolveShortCodeAsync(string shortCode)
    {
        var entry = await context.Links.FirstOrDefaultAsync(x => x.ShortCode == shortCode);

        return !string.IsNullOrEmpty(entry?.OriginalUrl) ? entry.OriginalUrl : null;

    }

    public async Task<List<LinkResult>> GetAllLinksAsync()
    {
        return await context.Links
            .Select(x => new LinkResult(
                 ($"{config["AppSettings:Domain"]}/{x.ShortCode}"),
                x.OriginalUrl,
                x.Clicks))
            .ToListAsync();
    }

    public async Task<bool> DeleteShortLinkAsync(string shortCode)
    {
        var entry = await context.Links.FirstOrDefaultAsync(x => x.ShortCode == shortCode);
        
        if (entry is null) return false;
        
        context.Links.Remove(entry);
        await context.SaveChangesAsync();
        return true;
    }
}