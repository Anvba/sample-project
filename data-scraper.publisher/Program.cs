using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using DataScraper.Model;
using Publisher.Model;
using DataScraper.DataAccess;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

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

			var serviceCollection = new ServiceCollection();

			serviceCollection.AddSingleton<IMongoDBConfig>(new MongoDBConfig
			{
				ConnectionString = "mongodb://mongo1:27017,mongo2:27017,mongo3:27017/datascraper?replicaSet=rs0",
				DatabaseName = "datascraper",
				CollectionName = "scraperDataModel"
			});
			serviceCollection.AddLogging(configure => configure.AddConsole());
			serviceCollection.AddSingleton<IMongoDBRepository<WebResourceModel>, MongoDBRepository<WebResourceModel>>();

    		var serviceProvider = serviceCollection.BuildServiceProvider();

			var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

			await busControl.StartAsync(source.Token);

			try
			{
				var enumerator = serviceProvider.GetService<IMongoDBRepository<WebResourceModel>>().Watch();
				while(enumerator.MoveNext())
				{
    				foreach(var doc in enumerator.Current)
					{
						await busControl.Publish<ScraperDataModel>((ScraperDataModel)doc.FullDocument);
					};
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
