using NunitPlayV2.TestHelper;



namespace NunitPlayV2.UITests
{
    [SetUpFixture]
    public class GlobalSetup
    {
        //[OneTimeSetUp]
        //public void BeforeAll()
        //{
        //    var resultsDir = Path.Combine(TestContext.CurrentContext.TestDirectory, "allure-results");
        //    Directory.CreateDirectory(resultsDir);

        //    var env = new XElement("environment",
        //        new XElement("parameter", new XElement("key", "Platform"), new XElement("value", "GitHub Actions")),
        //        new XElement("parameter", new XElement("key", "Runner"), new XElement("value", "Ubuntu-Latest"))
        //    );
        //    env.Save(Path.Combine(resultsDir, "environment.xml"));
        //}

        [OneTimeSetUp]
        public void InitializeReporting()
        {
            string buildId = Environment.GetEnvironmentVariable("GITHUB_RUN_NUMBER") ?? "Local-Run";
            AllureHelper.GenerateMetadata(buildId);
        }

    }

}
