using Microsoft.Playwright;


namespace NunitPlayV2.TestHelper
{
    public class TestUtil
    {
        public static string getBaseDirectory { get; } = Directory.GetCurrentDirectory().Split("bin")[0];
        public static async Task<byte[]> CaptureScreenshotBytes(IPage page, string screenshotName)
        {

            byte[] screenshotBytes = await page.ScreenshotAsync(new PageScreenshotOptions { FullPage = true });
            return screenshotBytes;
        }

        public static async Task<IReadOnlyList<IPage>> SwitchTab(IPage page, string title)
        {
            IReadOnlyList<IPage> Tabs = page.Context.Pages;
            return Tabs;
        }

        public static async Task<IPage> SwitchTab(IPage page, int tabIndex)
        {

            IReadOnlyList<IPage> Tabs = page.Context.Pages;
            return Tabs[tabIndex];
        }

        public static async Task<IPage> OpenNewTab(IPage page)
        {
            return await page.Context.NewPageAsync(); ;
        }

        

        public static string ProjectBaseDirectory
        {
            get { return AppContext.BaseDirectory; }
        }

        //public static void RegisterLog(string logtext)
        //{
        //    TestContext.Out.WriteLine(logtext);
        //    Log.Information(logtext);
        //}

        public static async Task SafeNavigate(IPage page, string url, int timeoutMilliseconds)
        {
            try
            {
                await page.GotoAsync(url, new PageGotoOptions
                {
                    Timeout = timeoutMilliseconds
                });
            }
            catch (TimeoutException)
            {
                // The C# code timed out, but the browser is still trying to load.
                // This JS call forces the browser to stop immediately.
                await page.EvaluateAsync("window.stop();");
                Console.WriteLine("Navigation timed out: Browser loading forced to stop.");
            }
        }


        public static async Task ScriptedBrowserLoading(IPage page,string url,int TimeInMilliSeconds)
        {
                // This script will wait for X ms and then stop the window
                string stopScript = @"(timeout) => {
                                            setTimeout(() => { window.stop();}, timeout);
                                     }";

                // Start the stop-timer in the background (no 'await' on the timer itself if it's internal)
                await page.EvaluateAsync(stopScript, 5000);

                // Proceed with navigation
                await page.GotoAsync(url);
            

        }




    }
}

