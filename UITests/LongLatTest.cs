using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using Microsoft.Playwright;
using NunitPlayV2.TestHelper;

namespace NunitPlayV2.UITests
{

    //[AllureNUnit]
    //[TestFixture]
    public class LongLatTest:BaseTest
    {

        //[Test]
        //[AllureName("Verify New User Creation")]
        //[AllureFeature("Test LongLat Functionality")]
        public async Task TestLogin()
        {
            //await Page.Context.SetGeolocationAsync(new Geolocation
            //{
            //    Latitude = 51.5074,
            //    Longitude = 0.1278
            //});
            await Page.GotoAsync("https://www.latlong.net/user/register", new() { Timeout = 120_000 });

            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Name", Exact = true }).ClickAsync();
            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Name", Exact = true }).FillAsync("tom");
            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Surname" }).ClickAsync();
            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Surname" }).FillAsync("hanks");
            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Email Address" }).ClickAsync();
            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Email Address" }).FillAsync("tomhanks02@yopmail.com");
            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Password", Exact = true }).ClickAsync();
            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Password", Exact = true }).FillAsync("Test@1234");
            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Repeat Password" }).ClickAsync();
            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Repeat Password" }).FillAsync("Test@1234");
            await Page.GetByRole(AriaRole.Checkbox, new() { Name = "By registering you accept the" }).CheckAsync();
            await Page.Locator("iframe[title='reCAPTCHA']").ContentFrame.GetByRole(AriaRole.Checkbox, new() { Name = "I'm not a robot" }).ClickAsync();
            await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();
            await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "My Account User Login" })).ToBeVisibleAsync();

            await Page.GetByText("Registered succesfully. Thank").ClickAsync();
            await Page.GetByRole(AriaRole.Link, new() { Name = "Geo Tools" }).ClickAsync();
            await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "My Account User Login" })).ToBeVisibleAsync();

            await Page.GetByRole(AriaRole.Link, new() { Name = "Latitude and Longitude Finder" }).ClickAsync();
            await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "My Account User Login" })).ToBeVisibleAsync();

            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Place Name" }).ClickAsync();
            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Place Name" }).FillAsync("Detroit");
            await Page.GetByRole(AriaRole.Button, new() { Name = "Find" }).ClickAsync();
            await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "My Account User Login" })).ToBeVisibleAsync();

            await Page.Locator("#latlongmap").ClickAsync();
            await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "×" })).ToBeVisibleAsync();

            await Page.GetByText("42.323080,-83.087239×+− OSM").ClickAsync();
        }


        [Test]
        [AllureFeature("Test LongLat Functionality")]
        public async Task GeoToolTests()
        {
            //await Page.GotoAsync("https://www.latlong.net/user/login", new() { Timeout = 120_000 });
            
            await TestUtil.SafeNavigate(Page, "https://www.latlong.net/user/login", 25_000);
            await Page.Locator("input#email").FillAsync("tomhanks01@yopmail.com");
            await Page.Locator("input#password1").FillAsync("Test@1234");
            
            //await Page.Locator("button[title='Login']").ClickAsync();
            //await Page.WaitForURLAsync(new Regex("*/user/"), new() { WaitUntil = WaitUntilState.DOMContentLoaded });
            
            await Page.RunAndWaitForRequestAsync(async () => {
                          await Page.Locator("button[title='Login']").ClickAsync(); 
                         },
                         request=>request.Url.Contains("/user"));




            await Page.Locator("a[title='Geographic Tools']").ClickAsync();
            await Page.Locator("h5:text-is('Timezone Finder and My Timezone')").ClickAsync();
            await Task.Delay(5000);
            //await TestUtil.CaptureScreenshotBytes(Page, "pagename.png");
            AllureApi.AddAttachment("123", "image/png", await TestUtil.CaptureScreenshotBytes(Page, "pagename.png"));

            await Page.Locator("h2:text-is('About the Timezone Finder Tool')").ScrollIntoViewIfNeededAsync();

            string LatData = await Page.Locator("input[id='lat']").GetAttributeAsync("placeholder")??null!;
            string LongData = await Page.Locator("input[id='lng']").GetAttributeAsync("placeholder")??null!;
            TestContext.Out.WriteLine($"Lat:{LatData} Long:{LongData}");




            var box = await Page.Locator("div#map").BoundingBoxAsync();
            TestContext.Out.WriteLine($"Height:{box!.Height} Width:{box.Width} x:{box.X} y:{box.Y}");

            //await Page.Locator("div#map").ScrollIntoViewIfNeededAsync(); // new() { Timeout=2000});
            await Page.Locator("div#map").ClickAsync(new () { Position = new Position { X = box.X +150,Y = box.Y +70 }});



            await Page.DragAndDropAsync("div#map", "div#map", new()
            {
                SourcePosition = new() {X=box.X,Y=box.Y },
                TargetPosition = new() {X=box.X+300, Y=box.Y+100 }
            });
            
            string LatData1 = await Page.Locator("input[id='lat']").GetAttributeAsync("placeholder") ?? null!;
            string LongData1 = await Page.Locator("input[id='lng']").GetAttributeAsync("placeholder") ?? null!;
            TestContext.Out.WriteLine($"Lat:{LatData1} Long:{LongData1}");

            await Task.Delay(5000);
            AllureApi.AddAttachment("123", "image/png", await TestUtil.CaptureScreenshotBytes(Page, "pagename.png"));

            /*
            await Page.GetByRole(AriaRole.Button, new() { Name = "Marker" }).ClickAsync();
            await Page.GetByRole(AriaRole.Button, new() { Name = "Marker" }).ClickAsync();
            await Page.Locator("#map").ClickAsync();
            await Page.GetByRole(AriaRole.Button, new() { Name = "Marker" }).ClickAsync(); 
             
            
             */




        }





    }
}
