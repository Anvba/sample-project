using System;
using System.Threading;
using System.Threading.Tasks;
using DataScraper.Model;
using MassTransit;


namespace scraped_data_processor
{
    class Program
    {
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

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await busControl.StartAsync(source.Token);
            try
            {
                Console.WriteLine("Press enter to exit");

                await Task.Run(() => Console.ReadLine());
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
            public async Task Consume(ConsumeContext<GameData> context)
            {
				Console.WriteLine("[Scraped Data Processor] Game Country: " + context.Message.GameCountry);	
				Console.WriteLine("[Scraped Data Processor] Game League: " + context.Message.GameLeague);	
				Console.WriteLine("[Scraped Data Processor] Game Date: " + context.Message.GameDate);	
				Console.WriteLine("[Scraped Data Processor] Game Time: " + context.Message.GameTime);
				Console.WriteLine("[Scraped Data Processor] Game First Team: " + context.Message.FirstTeam);
				Console.WriteLine("[Scraped Data Processor] Game Second Team: " + context.Message.SecondTeam);
				Console.WriteLine("[Scraped Data Processor] Game Score: "+ context.Message.GameScore);
				Console.WriteLine();
            }
        }
    }
}
