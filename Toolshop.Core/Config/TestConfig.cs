using Microsoft.Extensions.Configuration;

namespace Toolshop.Core.Config;

public static class TestConfig
{
    private static readonly Lazy<ToolshopSettings> _settings = new Lazy<ToolshopSettings>(InitSettings);

    public static ToolshopSettings Settings => _settings.Value;

    private static ToolshopSettings InitSettings()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables()
            .Build();
        
        var settings = config.GetSection("ToolshopSettings").Get<ToolshopSettings>();

        if (settings == null)
        {
            throw new InvalidOperationException("'ToolshopSettings' configuration section is missing");
        }
        settings.Validate();

        return settings;
    }
}