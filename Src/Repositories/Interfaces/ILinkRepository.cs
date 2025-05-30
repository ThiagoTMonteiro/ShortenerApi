using ShortenerApi.Models;

namespace ShortenerApi.Repositories.Interfaces;

public interface ILinkRepository
{
    Task<Link?> GetByShortCodeAsync(string shortCode);
    Task<List<Link>> GetAllAsync();
    Task AddAsync(Link link);
    Task DeleteAsync(Link link);
    Task SaveChangesAsync();
}