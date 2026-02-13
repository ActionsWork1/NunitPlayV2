using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using Microsoft.Playwright;
using NunitPlayV2.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NunitPlayV2.UITests
{
    
    //[AllureNUnit]
    public class HRMTests:BaseTest
    {

        public NewEmpPage? EmpPage;


        [SetUp]
        public async Task Init()
        {
            await Page.GotoAsync("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login", new() { Timeout = 120_000 });
            EmpPage = new NewEmpPage(Page);

        }
        //.oxd-loading.spinner

        //[Test,Category("HRM")]
        //[AllureName("Verify InValid Login")]
        //[AllureFeature("Login Functionality")]
        //public async Task TestLogin()
        //{
            

        //    await AllureApi.Step("Enter Credentials", async () => {
        //        await Page.FillAsync("input[name='username1']", "Admin");
        //        TestContext.Out.WriteLine("Enter Username data");

        //        await Page.FillAsync("input[name='password']", "admin123");
        //        TestContext.Out.WriteLine("Enter Password data");
        //    });
        //    await Page.ClickAsync("button[type='submit']");
        //    TestContext.Out.WriteLine("cliks on submit button");

        //    await Expect(Page).ToHaveURLAsync(new Regex("dashboard"));
        //    TestContext.Out.WriteLine("wait for dashboard page");
        //}


        [Test,Category("HRMTests")]
        [AllureName("Verify Add New Employee ")]
        [AllureFeature("Add New Employee Functionality")]
        public async Task TestCreateNewEmployee()
        {
            
            await AllureApi.Step("Login as admin", async () =>
            {
                await EmpPage!.LoginToHRM("Admin", "admin123");
            });

            string empID = new Random().NextInt64(1000, 5000).ToString();
            string UserName = "hank" + empID;

            await AllureApi.Step("Create New Employee", async () =>
            {
                await EmpPage!.createNewEmployee(UserName, empID);
                Console.WriteLine($"Credentials: {UserName}/Test1234");
                
            });

            await AllureApi.Step("Logout from application", async () =>
            {
                await EmpPage!.HRMLogout();
            });

            await AllureApi.Step("Login with new user", async () =>
            {
                await Expect(Page.GetByRole(AriaRole.Img, new() { Name = "company-branding" })).ToBeVisibleAsync(
                       new LocatorAssertionsToBeVisibleOptions()
                       {
                           Visible = true,
                           Timeout = 25_000
                       });
               

                await EmpPage!.LoginToHRM(UserName,"Test1234");
                await Page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "hank churchil" }).Locator("i").ClickAsync();
                await Expect(Page.GetByRole(AriaRole.Menuitem, new() { Name = "About" })).ToBeVisibleAsync(new() { Visible = true, Timeout = 25_000 });
                await Page.GetByRole(AriaRole.Menuitem, new() { Name = "Logout" }).ClickAsync();
                await Assertions.Expect(EmpPage._UserName_TXT).ToBeVisibleAsync(new() { Visible=true,Timeout=25_000});
                await Task.Delay(5000);

            });

            
            
        }





    
    }
}
