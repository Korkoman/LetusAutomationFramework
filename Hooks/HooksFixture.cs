using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Playwright;
using PlaywrightTests.Drivers;
using PlaywrightTests.Models;
using PlaywrightTests.Utilities;

namespace PlaywrightTests.Hooks
{   
    public class HooksFixture : IAsyncLifetime    {

        public IPage Page { get; private set; }
        public ILoggerFactory LoggerFactory { get; }
        public AppSettings Settings { get; }
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IBrowserContext _context;

        public HooksFixture() {

            LoggerFactory = LoggerFactoryBuilder.GetLoggerFactory();
            
        }

        public async Task InitializeAsync()
        {
            
            _browser = await DriverFactory.GetBrowserAsync();
            _context = await DriverFactory.GetContextAsync();
            Page = await DriverFactory.GetPageAsync();            
           
        }

        public async Task DisposeAsync()
        {
            await  Page.CloseAsync();
            await _context.CloseAsync();
            await DriverFactory.CloseAsync();
        }
    }
}
