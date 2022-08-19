using System;
using MassTransit;

namespace Domain.States
{
    public class AdvertisementState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public int CurrentState { get; set; }
    }
}
