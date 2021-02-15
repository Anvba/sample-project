using System;
using System.Linq;
using OpenQA.Selenium;
using DataScraper.Model;
using DataScraper.Logging;

namespace DataScraper.Extension
{
		public static class WebElementExtension
		{
			public static IWebElement GetElementBySelector(this IWebElement webElement, string selector, ILogger logger)
			{
				try
				{
					return webElement.FindElement(By.CssSelector(selector));
				}
				catch(Exception exc)
				{
					logger.LogError(exc);
					return null;
				}
			}

			public static IWebElement SelectElementFromSetOfSelectors(this IWebElement webElement, SelectorDescriptor[] selectors, ILogger logger)
			{
				IWebElement selectedWebElement = null; 
				var selectedMode = selectors
								.FirstOrDefault(model => 
								{
									selectedWebElement = GetElementBySelector(webElement, model.Selector, logger);

									if (selectedWebElement != null && 
										!String.IsNullOrWhiteSpace(selectedWebElement.Text))
									{
										return true;
									}

									return false; 
								});
				return selectedWebElement;
			}
		}
}
