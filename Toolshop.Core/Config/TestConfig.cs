using Microsoft.Extensions.Configuration;

namespace Toolshop.Core.Config;

public static class ToolshopConfig
{
    private static readonly Lazy<ToolshopSettings> _settings = new Lazy<ToolshopSettings>(InitSettings);

    public static ToolshopSettings Settings => _settings.Value;

    private static ToolshopSettings InitSettings()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables()
            .Build();
        
        var settings = config.GetSection("ToolshopSettings").Get<ToolshopSettings>();

        if (settings == null)
        {
            throw new InvalidOperationException("Секция конфигурации 'ToolshopSettings' не найдена.");
        }
        
        ArgumentNullException.ThrowIfNullOrEmpty(settings.UIBaseUrl);
        ArgumentNullException.ThrowIfNullOrEmpty(settings.ApiBaseUrl);
        ArgumentNullException.ThrowIfNullOrEmpty(settings.customer.Email);
        ArgumentNullException.ThrowIfNullOrEmpty(settings.customer.Password);

        return settings;
    }
}