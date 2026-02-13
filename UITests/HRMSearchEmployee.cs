using Microsoft.Playwright;
using NunitPlayV2.Pages;
using NunitPlayV2.UITests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NunitPlayV2.UITests
{
    public class HRMSearchEmployee:BaseTest
    {

        public NewEmpPage? EmpPage;


        [SetUp]
        public async Task Init()
        {
            await Page.GotoAsync("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login", new() { Timeout = 120_000 });
            EmpPage = new NewEmpPage(Page);

        }
        //.oxd-loading.spinner

        [Test, Category("HRMTests")]
        public async Task TestSearchEmployee()
        {
           await EmpPage!.LoginToHRM("Admin", "admin123");
           await Page.GetByText("PIM").ClickAsync();
           await Task.Delay(3000);

           IReadOnlyList<ILocator> textFields = await Page.Locator("div.oxd-table-filter-area").GetByRole(AriaRole.Textbox).AllAsync();

            await textFields[0].ClickAsync();
            await textFields[0].FillAsync("hank");

           // await Page.GetByLabel("Employee Name").ClickAsync();
           //await Page.GetByLabel("Employee Name").FillAsync("hanks");
           await Task.Delay(2000);
           await Page.Locator("div.oxd-form-actions").GetByRole(AriaRole.Button, new() { Name="Search"}).ClickAsync();     //.ClickAsync();
           await Task.Delay(5000);
        }







    }
}
