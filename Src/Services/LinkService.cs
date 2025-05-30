using Microsoft.EntityFrameworkCore;
using ShortenerApi.Models;
using ShortenerApi.Models.DTOs;
using ShortenerApi.Repositories.Interfaces;
using ShortenerApi.Services.Interfaces;

namespace ShortenerApi.Services;

public class LinkService(ILinkRepository repository, IAppSettingService settings, IShortCodeGeneratorService shortCodeGeneratorService) : ILinkService
{
    public async Task<string> CreateShortLinkTask(string originalUrl)
    {
        var shortCode = shortCodeGeneratorService.GenerateShortCode();
        var entry = new Link{ OriginalUrl = originalUrl, ShortCode = shortCode };
        await repository.AddAsync(entry);
        await repository.SaveChangesAsync();
        
        var domain = settings.GetDomain();
        return $"{domain}/{shortCode}";
    }

    public async Task<string?> ResolveShortCodeAsync(string shortCode)
    {
        var entry = await repository.GetByShortCodeAsync(shortCode);

        return !string.IsNullOrEmpty(entry?.OriginalUrl) ? entry.OriginalUrl : null;

    }

    public async Task<List<LinkResult>> GetAllLinksAsync()
    {
        var domain = settings.GetDomain();
        var all = await repository.GetAllAsync();
        return all.Select(x => new LinkResult(
                ($"{domain}/{x.ShortCode}"),
                        x.OriginalUrl,
                        x.Clicks))
                    .ToList();
    }

    public async Task<bool> DeleteShortLinkAsync(string shortCode)
    {
        var entry = await repository.GetByShortCodeAsync(shortCode);
        
        if (entry is null) return false;
        
        await repository.DeleteAsync(entry);
        await repository.SaveChangesAsync();
        return true;
    }
}