using Toolshop.Core.Config;

namespace Toolshop.Tests.Api;

[SetUpFixture]
public class GlobalSetup
{
    [OneTimeSetUp]
    public void ValidateConfig()
    {
        _ = TestConfig.Settings;
    }
}