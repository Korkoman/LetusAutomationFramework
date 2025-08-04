using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;
using PlaywrightTests.Utilities;

namespace PlaywrightTests.Drivers
{   
    public static class DriverFactory
    {
        private static IPlaywright? _playwright;
        private static IBrowser? _browser;

        public static async Task<IBrowser> GetBrowserAsync()
        {
            if (_browser == null)
            {
                var settings = ConfigReader.Settings;
                _playwright = await Playwright.CreateAsync();
                BrowserTypeLaunchOptions launchOptions = new()
                {
                    Headless = settings.Headless,
                    SlowMo = settings.SlowMo,
                };
                
                _browser = settings.Browser.ToLower() switch
                {
                    "firefox" => await _playwright.Firefox.LaunchAsync(launchOptions),
                    "webkit" => await _playwright.Webkit.LaunchAsync(launchOptions),
                    _ => await _playwright.Chromium.LaunchAsync(launchOptions), 
                };
            }

            return _browser;
        }

        public static async Task<IBrowserContext> GetContextAsync()
        {
            var browser = await GetBrowserAsync();
            return await browser.NewContextAsync(new BrowserNewContextOptions { 
            
                ViewportSize = new ViewportSize { 
                    Width = 1920,
                    Height = 1080
                },
                IgnoreHTTPSErrors = ConfigReader.Settings.IgnoreHttpsErrors             
            });
        }

        public static async Task<IPage> GetPageAsync()
        {
            var context = await GetContextAsync();
            return await context.NewPageAsync();
        }

        public static async Task CloseAsync()
        {
            if (_browser != null)
            {
                await _browser.CloseAsync();
                _playwright.Dispose();                
                _browser = null;
            }
        }
    }
}


