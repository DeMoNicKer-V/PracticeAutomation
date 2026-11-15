using Toolshop.Core.Api;
using Toolshop.Core.Config;

namespace Toolshop.Tests.Api;

public abstract class ApiTestBase
{
    protected ToolshopApi Api { get; private set; } = null!;

    [OneTimeSetUp]
    public void CreateApi()
    {
        Api = new ToolshopApi(TestConfig.Settings.ApiBaseUrl);
    }

    [OneTimeTearDown]
    public void DisposeApi()
    {
        Api.Dispose();
    }
}