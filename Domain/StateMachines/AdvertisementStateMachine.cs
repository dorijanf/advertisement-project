using Database;
using Domain.Services;
using Domain.States;
using MassTransit;
using Nest;
using SharedModels.Messages;

namespace Domain.StateMachines
{
    public class AdvertisementStateMachine : MassTransitStateMachine<AdvertisementState>
    {
        private readonly IPublisherService publisherService;
        private readonly IElasticClient elasticClient;

        public AdvertisementStateMachine(IPublisherService publisherService,
            IElasticClient elasticClient)
        {
            this.publisherService = publisherService;
            this.elasticClient = elasticClient;
        }

        public AdvertisementStateMachine()
        {
            InstanceState(x => x.CurrentState, Created, FailedToSynchronize, Synchronized);

            Event(() => CreateEvent, x => x.CorrelateById(context => context.Message.MessageId));
            Event(() => FailToSynchronizeEvent, x => x.CorrelateById(context => context.Message.MessageId));

            Initially(
                When(CreateEvent)
                    .ThenAsync(async context =>
                    {
                        var result = await elasticClient!.IndexDocumentAsync(context.Message.Advertisement);
                        if (!result.IsValid)
                        {
                            await publisherService!.Publish(new FailedToSynchronize
                            {
                                MessageId = context.Message.MessageId,
                                AdvertisementId = context.Message.Advertisement.Id
                            });
                        }
                    })
                    .TransitionTo(Synchronized));

            During(Created,
                When(FailToSynchronizeEvent)
                    .ThenAsync(async context =>
                    {
                        await publisherService!.Publish(new MarkAsFailedToSynchronize(context.Message.AdvertisementId));
                    })
                    .TransitionTo(FailedToSynchronize)
                    .Finalize());
        }

        public State Created { get; private set; }
        public State FailedToSynchronize { get; private set; }
        public State Synchronized { get; private set; }

        public MassTransit.Event<AdvertisementCreateMessage> CreateEvent { get; private set; }
        public MassTransit.Event<FailedToSynchronize> FailToSynchronizeEvent { get; private set; }
    }
}
