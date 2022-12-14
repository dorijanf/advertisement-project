using System;
using System.IO;
using System.Reflection;
using Database;
using Domain.Interfaces;
using Domain.Services;
using Domain.Subscribers;
using Domain.Validators;
using FluentValidation;
using Infrastructure.Email;
using Infrastructure.Publisher;
using Infrastructure.Repository;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Nest;
using RabbitMQ.Client;
using SharedModels.Dtos;
using SharedModels.Exceptions;
using SharedModels.Utils;

namespace backend_template.ServiceExtensions
{
    /// <summary>
    /// Extension class which makes the Startup class more readable by separating
    /// implementation logic of various services.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Configures elastic search with connection strings from AppSettings.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureElasticSearch(
            this IServiceCollection services, IConfiguration configuration)
        {
            var elasticSearchSettings = new ElasticSearchSettings();

            configuration.GetSection("ElasticSearch").Bind(elasticSearchSettings);

            services.AddSingleton(elasticSearchSettings);

            var settings = new ConnectionSettings(new Uri(elasticSearchSettings.ServerUrl))
                .DefaultIndex("advertisement")
                .DefaultMappingFor<AdvertisementDto>(m => m
                    .PropertyName(p => p.Id, "id")
                    .PropertyName(p => p.Title, "title")
                    .PropertyName(p => p.Content, "content")
                    .PropertyName(p => p.UserEmail, "userEmail")
                );

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);
        }

        /// <summary>
        /// Configures RabbitMq services with connection strings from AppSettings.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureRabbitMq(
            this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqSettings = new RabbitMqSettings();

            configuration.GetSection("RabbitMq").Bind(rabbitMqSettings);

            services.AddSingleton(rabbitMqSettings);

            services.AddMassTransit(config =>
            {
                config.AddConsumer<FavoriteConsumer>().Endpoint(e =>
                {
                    e.ConcurrentMessageLimit = 10;
                });

                config.AddConsumer<AdvertisementConsumer>(c =>
                    c.UseMessageRetry(r =>
                    {
                        r.Interval(2, TimeSpan.FromSeconds(5));
                        r.Handle<ElasticSearchError>();
                    }))
                    .Endpoint(e =>
                    {
                        e.ConcurrentMessageLimit = 10;
                    });

                config.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitMqSettings.HostName, rabbitMqSettings.VirtualHost,
                        h =>
                        {
                            h.Username(rabbitMqSettings.UserName);
                            h.Password(rabbitMqSettings.Password);
                        });

                    cfg.ExchangeType = ExchangeType.Direct;

                    cfg.ConfigureEndpoints(context);
                });
            });

            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
        }

        /// <summary>
        /// Configures the database context / database connection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureDatabaseContext(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AdvertisementContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Database"));
            });
        }

        /// <summary>
        /// Configures all scoped services for the API.
        /// </summary>
        /// <param name="services"></param>
        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IPublisherService, PublisherService>();

            services.AddScoped<IAdvertisementService, AdvertisementService>();

            services.AddScoped<IEmailSenderService, EmailSenderService>();

            services.AddTransient<IValidator<AdvertisementDto>, AdvertisementValidator>();

            services.AddTransient<IValidator<FavoriteDto>, FavoriteValidator>();

            services.AddScoped(typeof(Domain.Interfaces.IRepository<>), typeof(Repository<>));
        }

        /// <summary>
        /// Adds swagger configuration.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Advertisement API", Version = "v1" });
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}
