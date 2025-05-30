using Microsoft.EntityFrameworkCore;
using ShortenerApi.Models;
using ShortenerApi.Repositories.Interfaces;

namespace ShortenerApi.Repositories;

public class LinkRepository(AppDbContext context) : ILinkRepository
{
    public async Task<Link?> GetByShortCodeAsync(string shortCode) =>
       await context.Links.FirstOrDefaultAsync(x => x.ShortCode == shortCode);

    public async Task<List<Link>> GetAllAsync() => 
       await context.Links.ToListAsync();

    public async Task AddAsync(Link link) => await context.Links.AddAsync(link);

    public async Task DeleteAsync(Link link) => context.Links.Remove(link);

    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}