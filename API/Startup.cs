using backend_template.Database;
using Database;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Domain.Services;
using Microsoft.Extensions.Hosting;
using SharedModels.Utils;

namespace backend_template
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
            services.AddDbContext<AdvertisementContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Database"));
            });

            var rabbitMqSection = Configuration.GetSection("RabbitMq");
            var rabbitMqConfig = rabbitMqSection.Get<RabbitMqSettings>();

            services.AddMassTransit(c =>
            {
                c.AddConsumer<AdvertisementConsumer>();
            });

            services.AddSingleton(_ => Bus.Factory.CreateUsingRabbitMq(config =>
            {
                config.Host(rabbitMqConfig.HostName, rabbitMqConfig.VirtualHost,
                    h => {
                        h.Username(rabbitMqConfig.UserName);
                        h.Password(rabbitMqConfig.Password);
                    });

                config.ExchangeType = ExchangeType.Direct;
            }));

            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());

            services.AddScoped<IPublisherService, PublisherService>();

            services.AddScoped<IAdvertisementService, AdvertisementService>();

            services.AddLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
