using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using Microsoft.Playwright;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NunitPlayV2.Pages
{
    public class NewEmpPage:BasePage
    {
        private readonly IPage _page;

        // Login Page
        public ILocator _UserName_TXT;
        public ILocator _Password_TXT;
        public ILocator _Login_BTN;
        public ILocator _SidePanel_DIV;

        //Emp Page
        public ILocator _PIM_LINK;
        public ILocator _Empfirstname_TXT;
        public ILocator _Emplastname_TXT;
        public ILocator _EmpID_TXT;
        public ILocator _EmpEnableDisable_RD;
        public ILocator _EmpAdd_BTN;
        public ILocator _EmpUserName_TXT;
        public ILocator _EmpNewUserPwd_TXT;
        public ILocator _EmpSave_BTN;



        public NewEmpPage(IPage page):base(page)
        {
            this._page = page;
            //Login Page
            _UserName_TXT = _page.GetByRole(AriaRole.Textbox, new() { Name = "Username" });
            _Password_TXT = _page.GetByRole(AriaRole.Textbox, new() { Name = "Password" });
            _Login_BTN = _page.GetByRole(AriaRole.Button, new() { Name = "Login" });
            _SidePanel_DIV = _page.GetByRole(AriaRole.Navigation, new() { Name = "Sidepanel" });

            //Employee Page

            _PIM_LINK =_page.GetByRole(AriaRole.Link, new() { Name = "PIM" });

            _EmpAdd_BTN= _page.GetByRole(AriaRole.Button, new() { Name = " Add" });


            _Empfirstname_TXT=_page.GetByRole(AriaRole.Textbox, new() { Name = "First Name" });
            _Emplastname_TXT = _page.GetByRole(AriaRole.Textbox, new() { Name = "Last Name" });
            _EmpID_TXT = _page.GetByRole(AriaRole.Textbox).Nth(4);
            _EmpEnableDisable_RD = _page.Locator(".oxd-switch-input");
            _EmpUserName_TXT = _page.GetByRole(AriaRole.Textbox).Nth(5);
            _EmpNewUserPwd_TXT = _page.Locator("input[type='password']");
            _EmpSave_BTN = _page.GetByRole(AriaRole.Button, new() { Name = "Save" });

        }


        public async Task LoginToHRM(string username, string password)
        {
            await _UserName_TXT.ClickAsync();
            await _UserName_TXT.FillAsync(username);
            //await UserName_TXT.PressAsync("Tab");
            await _Password_TXT.FillAsync(password);
            await _Login_BTN.ClickAsync();
            await Assertions.Expect(_SidePanel_DIV).ToBeVisibleAsync(new () { Visible = true, Timeout = 25_000 });
        }





        public async Task createNewEmployee(string UserName,string empID)
        {
            await _PIM_LINK.ClickAsync();
            //await this._page.GotoAsync("/pim/viewEmployeeList");
            await Assertions.Expect(_SidePanel_DIV).ToBeVisibleAsync(new() { Visible = true, Timeout = 25_000 });

            await _EmpAdd_BTN.ClickAsync();
            await Assertions.Expect(_SidePanel_DIV).ToBeVisibleAsync(new() { Visible = true, Timeout = 25_000 });

            await _Empfirstname_TXT.ClickAsync();
            await _Empfirstname_TXT.FillAsync("hank");
            await _Emplastname_TXT.ClickAsync();
            await _Emplastname_TXT.FillAsync("churchil");


            await _EmpID_TXT.FillAsync(empID);
            await _EmpSave_BTN.ClickAsync();
            await _EmpEnableDisable_RD.ClickAsync();

            await Assertions.Expect(this._page.GetByRole(AriaRole.Radio, new() { Name = "Enabled" })).ToBeVisibleAsync(new () { Visible = true });

            await _EmpUserName_TXT.ClickAsync();
            await _EmpUserName_TXT.FillAsync(UserName);
            await _EmpNewUserPwd_TXT.First.ClickAsync();
            await _EmpNewUserPwd_TXT.First.FillAsync("Test1234");
            await _EmpNewUserPwd_TXT.Nth(1).ClickAsync();
            await _EmpNewUserPwd_TXT.Nth(1).FillAsync("Test1234");
            await _EmpSave_BTN.ClickAsync();

            //await Expect(Page.Locator(".oxd-loading.spinner")).Not.ToBeVisibleAsync(
            //          new LocatorAssertionsToBeVisibleOptions() 
            //          { 
            //              Visible = true, 
            //              Timeout = 35_000 
            //          });
            await Assertions.Expect(this._page.Locator("div.orangehrm-edit-employee-navigation")).ToBeVisibleAsync(
                new()
                {
                    Visible = true,
                    Timeout = 45_000
                });
            Console.WriteLine($"Credentials: {UserName}/Test1234");
        }

        public async Task HRMLogout()
        {
            //await Page.GotoAsync("/pim/viewPersonalDetails/empNumber/223",new PageGotoOptions() { Timeout=35_000});
            await Assertions.Expect(_SidePanel_DIV).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions() { Visible = true, Timeout = 25_000 });
            //await Page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "Haleema user" }).Locator("i").ClickAsync();
            await _page.Locator("span.oxd-userdropdown-tab").ClickAsync();
            //await _page.Locator("p.oxd-userdropdown-name").ClickAsync();
            await Assertions.Expect(_page.GetByRole(AriaRole.Menuitem, new() { Name = "About" })).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions() { Visible = true, Timeout = 25_000 });
            await _page.GetByRole(AriaRole.Menuitem, new() { Name = "Logout" }).ClickAsync();
        }

    }
}

//await Page.GotoAsync("/dashboard/index");