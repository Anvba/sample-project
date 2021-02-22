using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DataScraper.DataAccess;
using Microsoft.Extensions.Options;
using DataAccess.AdminAPI.Model;

namespace data_scraper.admin_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

			services.Configure<MongoDBConfig>(Configuration.GetSection(nameof(MongoDBConfig)));

    		services.AddSingleton<IMongoDBConfig>(sp => sp.GetRequiredService<IOptions<MongoDBConfig>>().Value);
        
			services.AddSingleton<IMongoDBRepository<WebResourceModel>, MongoDBRepository<WebResourceModel>>();
			services.AddScoped<IScraperModelDataService<WebResourceModel>, ScraperModelDataService<WebResourceModel>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

//            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
