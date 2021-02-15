using System;

namespace DataScraper.Logging
{
	public interface ILogger
	{
		void LogInfo(string message);

		void LogError(Exception exc);
	}
}
