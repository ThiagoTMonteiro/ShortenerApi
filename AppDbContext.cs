using Microsoft.EntityFrameworkCore;
using ShortenerApi.Models;

namespace ShortenerApi;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Link> Links => Set<Link>();
    public DbSet<LinkMetric> LinkMetrics => Set<LinkMetric>();
}