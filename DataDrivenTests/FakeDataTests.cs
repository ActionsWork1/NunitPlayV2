using Allure.NUnit;
using Microsoft.Playwright;
using NunitPlayV2.TestHelper;


namespace NunitPlayV2.DataDrivenTests
{
    //[TestFixture]
    //[AllureNUnit]
    public class FakeDataTests:BaseTest
    {
        //private IBrowser? _browser;
        //private IPage? Page;

        [SetUp]
        public async Task Setup()
        {
            //var playwright = await Playwright.CreateAsync();
            //_browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            //var context = await _browser.NewContextAsync();
            //Page = await context.NewPageAsync();
        }

        //[TearDown]
        //public async Task Teardown()
        //{
        //    await _browser!.CloseAsync();
        //}

        private static IEnumerable<UserTestData1> RegistrationData = DataGenerator.GetUserTests(5);

        [Test, TestCaseSource(nameof(RegistrationData))]
        public async Task UserShouldBeAbleToRegister(UserTestData1 testData)
        {
            TestContext.Out.WriteLine($"username:{testData.Email}/Password:{testData.FirstName!}");

            await Page!.GotoAsync("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login");

            await Page.FillAsync("input[name='username']", testData.Email!);
            await Page.FillAsync("input[name = 'password']", testData.FirstName!);
            await Task.Delay(1500);

        }





    }//test-class

    //public static class DataGenerator
    //{
    //    public static List<UserTestData1> GetUserTests(int count)
    //    {
    //        return new Bogus.Faker<UserTestData1>()
    //            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
    //            .RuleFor(u => u.LastName, f => f.Name.LastName())
    //            .RuleFor(u => u.Email, f => f.Internet.Email())
    //            .RuleFor(u => u.Password, f => f.Internet.Password(10))
    //            .Generate(count);
    //    }
    //}
    //public class UserTestData1
    //{
    //    public string? FirstName { get; set; }
    //    public string? LastName { get; set; }
    //    public string? Email { get; set; }
    //    public string? Password { get; set; }
    //}

}//namespace
