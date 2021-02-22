using System;
using System.Threading;
using System.Threading.Tasks;
using DataScraper.Model;
using DataScraper.WebResourceConsumer;
using DataScraper.Logging;
using MassTransit;

namespace DataScraper
{
	public class OnDataItemImpl : IOnDataItem
	{
		private IBusControl _busControl;

		private CancellationTokenSource _source;
		
		public async Task Initialize()
		{
			var host = Environment.GetEnvironmentVariable("rabbitmq", EnvironmentVariableTarget.Process);
			var rabbitUser = Environment.GetEnvironmentVariable("r_user", EnvironmentVariableTarget.Process);
			var rabbitPass = Environment.GetEnvironmentVariable("r_pass", EnvironmentVariableTarget.Process);

			Console.WriteLine("[Game Data Publisher] Rabbit MQ connecion detaisl: host: {0}, user: {1}, pass: {2}", host, rabbitUser, rabbitPass);

			_busControl = Bus.Factory.CreateUsingRabbitMq(cfg => 
			{
				cfg.Host(host, "/", h =>
                {
                    h.Username(rabbitUser);
                    h.Password(rabbitPass);
                });
			});

			_source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

			await _busControl.StartAsync(_source.Token);
		}

		public async Task OnDataItem(GameData gameData)
		{
			try
			{
				Console.WriteLine("Game Country: " + gameData.GameCountry);	
				Console.WriteLine("Game League: " + gameData.GameLeague);	
				Console.WriteLine("Game Date: " + gameData.GameDate);	
				Console.WriteLine("Game Time: " + gameData.GameTime);
				Console.WriteLine("Game First Team: " + gameData.FirstTeam);
				Console.WriteLine("Game Second Team: " + gameData.SecondTeam);
				Console.WriteLine("Game Score: "+ gameData.GameScore);
				Console.WriteLine();

				await _busControl.Publish<GameData>(gameData);
			}
			catch(Exception exc)
			{
				Console.WriteLine("Error message: {0}", exc.Message);	
			}
		}	
		public async Task Destroy()
		{
			await _busControl.StopAsync();
		}
	}

    class Program
    {
		public static IOnDataItem OnDataItem = new OnDataItemImpl();

		public static async Task Main()
        {
			var host = Environment.GetEnvironmentVariable("rabbitmq", EnvironmentVariableTarget.Process) ?? "localhost";
			var rabbitUser = Environment.GetEnvironmentVariable("r_user", EnvironmentVariableTarget.Process) ?? "guest";
			var rabbitPass = Environment.GetEnvironmentVariable("r_pass", EnvironmentVariableTarget.Process) ?? "guest";

			Console.WriteLine("Rabbit MQ connecion detaisl: host: {0}, user: {1}, pass: {2}", host, rabbitUser, rabbitPass);

			await OnDataItem.Initialize();

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
				cfg.Host(host, "/", h =>
                {
                    h.Username(rabbitUser);
                    h.Password(rabbitPass);
                });

                cfg.ReceiveEndpoint("event-listener", e =>
                {
                    e.Consumer<ScraperDataModelConsumer>();
                });
            });

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await busControl.StartAsync(source.Token);
            try
            {
                Console.WriteLine("Press enter to exit");

				while(true)
				{
                	await Task.Run(() => Console.ReadLine());
				}
            }
			catch(Exception exc)
			{
				Console.WriteLine("Error message: {0}", exc.Message);	
			}
            finally
            {
                await busControl.StopAsync();
				await Program.OnDataItem.Destroy();
            }
        }

        class ScraperDataModelConsumer : IConsumer<ScraperDataModel>
        {
            public async Task Consume(ConsumeContext<ScraperDataModel> context)
            {
				var driverType = Environment.GetEnvironmentVariable("driver_type", EnvironmentVariableTarget.Process) ?? "local";
				var remoteDriverDomainName = Environment.GetEnvironmentVariable("driver_domain_name", EnvironmentVariableTarget.Process) ?? "localhost";
				var webResource = new WebResource(Program.OnDataItem, new Logger());

				if (string.Equals(driverType, "local"))
				{
					webResource.Initialize(context.Message, remoteDriverDomainName);
				}
				else if (string.Equals(driverType, "remote"))
				{
					webResource.Initialize(context.Message, remoteDriverDomainName, true);
				}
				else
				{
					throw new NotImplementedException("Provided Selenium Web Driver Type is not implemented");
				}

				await webResource.CollectData();
            }
        }
	}
}
