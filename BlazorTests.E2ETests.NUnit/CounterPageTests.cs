using Microsoft.Playwright;

namespace BlazorTests.E2ETests.NUnit
{
    public class Tests
    {

        [Test]
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

        [Test]
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