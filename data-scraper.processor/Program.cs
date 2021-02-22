using System;
using System.Threading;
using System.Threading.Tasks;
using DataScraper.Model;
using DataScraper.DataAccess;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.DependencyInjection;
using DataScraper.Processor;

namespace DataScraper.Processor 
{
    class Program
    {
		public static IServiceProvider ServiceProvider = null;

        static async Task Main()
        {
			var host = Environment.GetEnvironmentVariable("rabbitmq", EnvironmentVariableTarget.Process) ?? "localhost";
			var rabbitUser = Environment.GetEnvironmentVariable("r_user", EnvironmentVariableTarget.Process) ?? "guest";
			var rabbitPass = Environment.GetEnvironmentVariable("r_pass", EnvironmentVariableTarget.Process) ?? "guest";

			Console.WriteLine("[Scraped Data Processor] Rabbit MQ connecion detaisl: host: {0}, user: {1}, pass: {2}", host, rabbitUser, rabbitPass);

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
				cfg.Host(host, "/", h =>
                {
                    h.Username(rabbitUser);
                    h.Password(rabbitPass);
                });

                cfg.ReceiveEndpoint("scraper-data-processor", e =>
                {
                    e.Consumer<GameDataConsumer>();
                });
            });

			var serviceCollection = new ServiceCollection();

			serviceCollection.AddSingleton<IMongoDBConfig>(new MongoDBConfig
			{
				ConnectionString = "mongodb://mongo1:27017,mongo2:27017,mongo3:27017/datascraper?replicaSet=rs0",
				DatabaseName = "datascraper",
				CollectionName = "gameData"
			});
			serviceCollection.AddLogging(configure => configure.AddConsole());
			serviceCollection.AddSingleton<IMongoDBGameRepository<GameDataModel>, MongoDBGameRepository<GameDataModel>>();

    		ServiceProvider = serviceCollection.BuildServiceProvider();

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await busControl.StartAsync(source.Token);
            try
            {
                Console.WriteLine("Press enter to exit");

				while (true)
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
            }
        }

        class GameDataConsumer : IConsumer<GameData>
        {
            public Task Consume(ConsumeContext<GameData> context)
            {

				if (string.IsNullOrWhiteSpace(context.Message.GameCountry) &&
					string.IsNullOrWhiteSpace(context.Message.GameLeague) &&
					string.IsNullOrWhiteSpace(context.Message.GameDate) &&
					string.IsNullOrWhiteSpace(context.Message.GameTime) &&
					string.IsNullOrWhiteSpace(context.Message.FirstTeam) &&
					string.IsNullOrWhiteSpace(context.Message.SecondTeam) &&
					string.IsNullOrWhiteSpace(context.Message.GameScore))
				{
					return Task.CompletedTask;
				}
				

				var mongoRepository  = Program.ServiceProvider.GetService<IMongoDBGameRepository<GameDataModel>>();
				var uid = string.Format("{0}{1}{2}", context.Message.GameLeague, context.Message.FirstTeam, context.Message.SecondTeam);

				if (mongoRepository.Find(uid) != null)
				{
					return Task.CompletedTask;
				}

				var gameDataModel = new GameDataModel
				{
                    GameCountry = context.Message.GameCountry,
                    GameLeague = context.Message.GameLeague, 
                    GameDate = context.Message.GameDate,
                    GameTime = context.Message.GameTime,
                    FirstTeam = context.Message.FirstTeam,
                    SecondTeam = context.Message.SecondTeam, 				
                    GameScore = context.Message.GameScore,
					UID = uid,
					Version = "1"
				};

				mongoRepository.CreateDocument(gameDataModel);

				Console.WriteLine("[Scraped Data Processor] Game Id: " + gameDataModel.Id);	
				Console.WriteLine("[Scraped Data Processor] Game Country: " + gameDataModel.GameCountry);	
				Console.WriteLine("[Scraped Data Processor] Game Country: " + gameDataModel.GameCountry);	
				Console.WriteLine("[Scraped Data Processor] Game League: " + gameDataModel.GameLeague);	
				Console.WriteLine("[Scraped Data Processor] Game Date: " + gameDataModel.GameDate);	
				Console.WriteLine("[Scraped Data Processor] Game Time: " + gameDataModel.GameTime);
				Console.WriteLine("[Scraped Data Processor] Game First Team: " + gameDataModel.FirstTeam);
				Console.WriteLine("[Scraped Data Processor] Game Second Team: " + gameDataModel.SecondTeam);
				Console.WriteLine("[Scraped Data Processor] Game Score: "+ gameDataModel.GameScore);
				Console.WriteLine();

				return Task.CompletedTask;
            }
        }
    }
}
