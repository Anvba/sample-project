using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace DataScraper.Extension
{
	public static class WebDriverExtension
	{
		public static IWebDriver GetChromeDriver(this string url)
		{
			var chromeOptions = new ChromeOptions();
			chromeOptions.AddArguments("headless");
			chromeOptions.AddArgument("--disable-javascript");

			var driver = new ChromeDriver(chromeOptions);
			driver.Navigate().GoToUrl(url);

			return driver;
		}	
	}	
}
