using ShortenerApi.Models;
using ShortenerApi.Models.DTOs;

namespace ShortenerApi.Services.Interfaces;

public interface ILinkService
{
    Task<string> CreateShortLinkTask(string originalUrl);
    Task<string?> ResolveShortCodeAsync(string shortCode);
    Task<List<LinkResult>> GetAllLinksAsync();
    Task<bool> DeleteShortLinkAsync(string shortCode);
}
