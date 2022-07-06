using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.VisualBasic;
using Xunit;

namespace BlazorTests.E2ETests
{
    public class IndexTitleTests
    {
        [Fact]
        public async Task ShouldNavigateTwoTasks()
        {
            Parallel.ForEach(
                Enumerable.Range(1, 2),
                iteration =>
                {
                    Run(iteration).Wait();
                }
            );
        }

        [Fact]
        public async Task ShouldNavigateFourTasks()
        {
            Parallel.ForEach(
                Enumerable.Range(1, 4),
                iteration =>
                {
                    Run(iteration).Wait();
                }
            );
        }

        public async Task Run(int iteration)
        {
            using var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 3000
            });

            var page = await browser.NewPageAsync();

            await page.GotoAsync("http://localhost:81/");

            await page.ClickAsync("#NavButtonTrue");

            await page.WaitForSelectorAsync("#CounterButton");
        }
    }
}