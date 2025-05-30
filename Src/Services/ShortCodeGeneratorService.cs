using ShortenerApi.Services.Interfaces;

namespace ShortenerApi.Services;

public class ShortCodeGeneratorService : IShortCodeGeneratorService
{
    public string GenerateShortCode() => Guid.NewGuid().ToString()[..6];
}