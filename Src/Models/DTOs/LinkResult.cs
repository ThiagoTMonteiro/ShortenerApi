namespace ShortenerApi.Models.DTOs;

public record LinkResult(string ShortUrl, string OriginalUrl, int Clicks);