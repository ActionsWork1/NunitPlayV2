//using Allure.NUnit;
//using Bogus; // Faker library
//using Microsoft.Playwright;
//using NUnit.Framework;
//using System.Threading.Tasks;

//namespace NPlayV2.DataDrivenTests
//{
//    [TestFixture]
//    [AllureNUnit]
//    public class DataDrivenTests  //:BaseTest
//    {
//       private IBrowser? _browser;
//       private IPage? _page;

//       [SetUp]
//       public async Task Setup()
//       {
//            //_page = Page;
//            var playwright = await Playwright.CreateAsync();
//            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
//            var context = await _browser.NewContextAsync();
//            _page = await context.NewPageAsync();
//        }

//        [Test, TestCaseSource(typeof(TestData), nameof(TestData.UserData))]
//        public async Task RegisterUserTest(string name, string email, string password)
//        {
//            await _page.GotoAsync("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login");
//            await _page.FillAsync("input[name='username']", name);
//            await _page.FillAsync("input[name='password']", password);
//            await _page.WaitForTimeoutAsync(3000);
//        }

//    }

//    public class TestData
//    {
//        public static IEnumerable<TestCaseData> UserData()
//        {
//            var faker = new Faker();

//            for (int i = 0; i < 3; i++)
//            {
//                yield return new TestCaseData(
//                    faker.Name.FullName(),
//                    faker.Internet.Email(),
//                    faker.Internet.Password()
//                );
//            }
//        }
//    }
//}







