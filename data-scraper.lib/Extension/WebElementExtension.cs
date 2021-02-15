using System;
using System.Linq;
using OpenQA.Selenium;
using DataScraper.Model;

namespace DataScraper.Extension
{
		public static class WebElementExtension
		{
				public static IWebElement GetElementBySelector(this IWebElement webElement, string selector)
				{
						try
						{
							return webElement.FindElement(By.CssSelector(selector));
						}
						catch(Exception exc)
						{
							Console.WriteLine("Selection Error: " + exc.Message);
							return null;
						}
				}

				public static IWebElement SelectElementFromSetOfSelectors(this IWebElement webElement, SelectorDescriptor[] selectors)
				{
						IWebElement selectedWebElement = null; 
						var selectedMode = selectors
										.FirstOrDefault(model => 
										{
											selectedWebElement = GetElementBySelector(webElement, model.Selector);

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
