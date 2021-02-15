using System;

namespace DataScraper.Logging
{
	public class Logger : ILogger
	{
		public void LogInfo(string message)
		{
			Console.WriteLine(message);
		}
		public void LogError(Exception exc)
		{
			Console.WriteLine(exc.Message);
		}
	}
}
