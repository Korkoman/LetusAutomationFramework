using Microsoft.Extensions.Logging;
using PlaywrightTests.Hooks;
using PlaywrightTests.Pages;
using PlaywrightTests.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightTests.Tests
{
    
    public class TestTest : HooksBase
    {
        private readonly ILogger logger;
        private readonly DemoQaMainPage demoqaMainPage;
        

        public TestTest(HooksFixture fixture): base(fixture) {
         logger = (ILogger)LoggerFactoryBuilder.CreateLogger(this.GetType());
            demoqaMainPage = new DemoQaMainPage(Page);

        }

       
        [Fact]
        public async Task TestingTestTest1() {

            TestConextHelper.SetTestName();            

            await RunSafe(async () => {
                var title = await Page.TitleAsync();
                logger.LogInformation("Este es el título de la página:{title} ", title);
                await demoqaMainPage.elementsModule();
                Assert.Equal("DEMOQAS", await Page.TitleAsync());
                
            });
           
        }

        [Fact]
        public async Task TestingTestTest2()
        {
            TestConextHelper.SetTestName();
            logger.LogInformation("Iniciando {test} ", TestContextProvider.TestName);

            await RunSafe(async () => {
                var title = await Page.TitleAsync();
                logger.LogInformation("Este es el título de la página:{title} ", title);
                await demoqaMainPage.elementsModule();
                Assert.Equal("DEMOQAt", await Page.TitleAsync());
                logger.LogInformation("Finalizando {test} ", TestContextProvider.TestName);
            });
        }
    }
}
