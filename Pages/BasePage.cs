using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NunitPlayV2.Pages
{
    public class BasePage
    {
        private readonly IPage _page;
        public BasePage(IPage page)
        {
            this._page = page;
        }




    }
}
