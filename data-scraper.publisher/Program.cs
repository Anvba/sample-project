using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using DataScraper.Model;

namespace data_scraper.publisher
{
	public class Program
	{
		public static async Task Main()
		{
			var host = Environment.GetEnvironmentVariable("rabbitmq", EnvironmentVariableTarget.Process);
			var rabbitUser = Environment.GetEnvironmentVariable("r_user", EnvironmentVariableTarget.Process);
			var rabbitPass = Environment.GetEnvironmentVariable("r_pass", EnvironmentVariableTarget.Process);

			Console.WriteLine("Rabbit MQ connecion detaisl: host: {0}, user: {1}, pass: {2}", host, rabbitUser, rabbitPass);

			var busControl = Bus.Factory.CreateUsingRabbitMq(cfg => 
			{
				cfg.Host(host, "/", h =>
                {
                    h.Username(rabbitUser);
                    h.Password(rabbitPass);
                });
			});

			var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

			await busControl.StartAsync(source.Token);
			try
			{
				while (true)
				{
					await busControl.Publish<ScraperDataModel>(new ScraperDataModel());
					await Task.Delay(TimeSpan.FromSeconds(300));
				}
			}
			catch(Exception exc)
			{
				Console.WriteLine("Error message: {0}", exc.Message);	
			}
			finally
			{
				await busControl.StopAsync();
			}
		}
	}
}
