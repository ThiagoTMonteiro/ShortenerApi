using ShortenerApi.Services.Interfaces;

namespace ShortenerApi.Services;

public class AppSettingService(IConfiguration config) : IAppSettingService
{
    public string GetDomain() => config["AppSettings:Domain"] ?? "https://localhost";
}