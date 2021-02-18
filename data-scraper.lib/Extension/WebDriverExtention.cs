using System;
using OpenQA.Selenium.Remote;
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

		public static IWebDriver GetRemoteChromeDriver(this string url, string remoteDriverDomainName)
		{
			var chromeOptions = new ChromeOptions();
			chromeOptions.AddArguments("headless");
			chromeOptions.AddArgument("--disable-javascript");

			var driver = new RemoteWebDriver(new Uri(string.Format("http://{0}:4444/wd/hub", remoteDriverDomainName)), chromeOptions);
			driver.Navigate().GoToUrl(url);

			return driver;
		}	
	}	
}
