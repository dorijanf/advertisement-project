using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace backend_template.BackgroundWorkers
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBusControl _busControl;
        private readonly IServiceProvider _serviceProvider;
        public Worker(IServiceProvider serviceProvider, ILogger<Worker> logger, IBusControl busControl)
        {
            _logger = logger;
            _busControl = busControl;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                //var productChangeHandler = _busControl.ConnectReceiveEndpoint(_queueSettings.QueueName, x =>
                //{
                //    x.Consumer<AdvertisementConsumer>(_serviceProvider);
                //});

                //await productChangeHandler.Ready;
            }
            catch (Exception ex)
            {
                _logger.LogError("DataProcessor cannot be started.", ex);
            }
        }
    }
}
