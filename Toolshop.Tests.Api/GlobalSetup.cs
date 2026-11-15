using Toolshop.Core.Config;

namespace Toolshop.Tests.Api;

[SetUpFixture]
public class GlobalSetup
{
    [OneTimeSetUp]
    public void ValidateConfig()
    {
        _ = TestConfig.Settings;
        TestContext.Progress.WriteLine($"API: {TestConfig.Settings.ApiBaseUrl}");
    }
}