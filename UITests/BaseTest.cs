using Allure.Net.Commons;
using Allure.NUnit;
using Microsoft.Playwright;



namespace NunitPlayV2.UITests
{

    // NOTE: Removed duplicate [AllureNUnit] attribute from the class.
    // If you still see CS0579, search the solution for other uses of AllureNUnit (including assembly-level).
    [TestFixture]
    [AllureNUnit]
    public class BaseTest : PageTest
    {
        public BrowserTypeLaunchOptions? BrowserTypeLaunchOptions { get; set; }

        //public BaseTest()
        //{
        //    // FIX 1: Do NOT use 'override'. 
        //    // Just assign the property in the constructor.
        //    BrowserTypeLaunchOptions = new BrowserTypeLaunchOptions
        //    {
        //        Headless = false,
        //        SlowMo = 100,
        //    };
        //}

        public override BrowserNewContextOptions ContextOptions()
        {
            return new BrowserNewContextOptions
            {
                ColorScheme = ColorScheme.Light,
                ViewportSize = new ViewportSize { Width = 1920, Height = 1080 },
                //BaseURL = "https://www.example.com",
                Permissions = new[] { "geolocation" },
                //Geolocation = new Geolocation() { Latitude = 41.889938, Longitude = 12.492507 },
                TimezoneId = "America/Denver"
               

            };
        }








        [SetUp]
        public async Task StartTrace()
        {
            await Context.Tracing.StartAsync(new() { Screenshots = true, Snapshots = true });
        }

        [TearDown]
        public async Task EndTrace()
        {
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "trace.zip");
            await Context.Tracing.StopAsync(new() { Path = path });

            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                AllureApi.AddAttachment("Debug Trace", "application/zip", path);
            }
        }
    }

}
