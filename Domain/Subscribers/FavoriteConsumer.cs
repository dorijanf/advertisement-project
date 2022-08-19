using System;
using System.Threading.Tasks;
using Domain.Services;
using MassTransit;
using Microsoft.Extensions.Logging;
using SharedModels.Messages;
using SharedModels.Utils;

namespace Domain.Subscribers
{
    /// <summary>
    /// Mass transit consumer service which consumes the <see cref="FavoriteCreateMessage"/>
    /// and gets triggered whenever a new advertisement gets added as a favorite by a user.
    /// </summary>
    public class FavoriteConsumer : IConsumer<FavoriteCreateMessage>
    {
        private readonly IEmailSenderService emailSenderService;
        private readonly ILogger<FavoriteConsumer> logger;

        public FavoriteConsumer(IEmailSenderService emailSenderService, ILogger<FavoriteConsumer> logger)
        {
            this.emailSenderService = emailSenderService;
            this.logger = logger;
        }

        /// <summary>
        /// When we receive the message we need to send a mock email to the user.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<FavoriteCreateMessage> context)
        {
            try
            {
                await emailSenderService.SendEmail(new EmailObject
                {
                    UserEmail = context.Message.UserEmail,
                    Content = $"Oglas {context.Message.Title} s identifikacijskim brojem {context.Message.AdvertisementId} je uspješno dodan u favorite."
                });
            }
            catch (Exception ex)
            {
                logger.LogError($"Error when consuming favorite added message: {ex.Message}");
            }
        }
    }
}
