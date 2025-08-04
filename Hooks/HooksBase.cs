using Microsoft.Extensions.Logging;
using Microsoft.Playwright;
using PlaywrightTests.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightTests.Hooks
{
    public abstract class HooksBase : IClassFixture<HooksFixture>, IAsyncLifetime
    {
        protected readonly IPage Page;
        private readonly HooksFixture HooksFixture;
        protected readonly ILogger Logger;

        protected HooksBase(HooksFixture fixture)
        {
            Page = fixture.Page;
            HooksFixture = fixture;
            Logger = LoggerFactoryBuilder.CreateLogger(this.GetType());            
           
        }

        public virtual async Task InitializeAsync()        {
                  
            await Page.GotoAsync(ConfigReader.Settings.BaseUrl);
        }

        public virtual async Task DisposeAsync()
        {          
            Logger.LogInformation("Test finalizó correctamente.");                     
        }
        
        protected async Task RunSafe(Func<Task> testBody)
        {
            try
            {
                await testBody();
            }
            catch (Exception ex)
            {
                
                Logger.LogError(ex, "Test falló con excepción.");
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..",".."));
                var screenshotDir = Path.Combine(projectRoot,"Screenshots",this.GetType().Name,TestContextProvider.TestName);
                Directory.CreateDirectory(screenshotDir);

                var screenshotPath = Path.Combine(screenshotDir, $"{TestContextProvider.TestName}_{timestamp}.png");

                await Page.ScreenshotAsync(new PageScreenshotOptions
                {
                    Path = screenshotPath,
                    FullPage = true
                });

                Logger.LogError("Test falló — Screenshot guardado en: {Path}", screenshotPath);
                throw;
            }
        }


    }
}

