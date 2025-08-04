using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightTests.Pages
{
    public class DemoQaMainPage
    {
        private readonly IPage _page;

        public DemoQaMainPage(IPage page){
            _page = page;
        }

        public ILocator elementsLink => _page.Locator("#app > div > div > div.home-body > div > div:nth-child(1)");

        public async Task elementsModule() {
            await elementsLink.ClickAsync();
        }
    }
}
