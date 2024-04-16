using MassTransit;
using SharedMessage.Commands;
using SharedMessage.Events;

namespace OrderSaga.Worker.OrderSaga
{
    internal class OrderStateMachine : MassTransitStateMachine<OrderStateInstance>
    {
        //States
        public State OrderCreated { get; set; }

        public State StockReserved { get; set; }
        public State StockReservationFailed { get; set; }
        public State PaymentCompleted { get; set; }
        public State PaymentFailed { get; set; }

        // Command
        public Event<CreateOrderCommand> CreateOrderCommand { get; set; }

        //Events
        public Event<StockReservedEvent> StockReservedEvent { get; set; }

        public Event<StockReservationFailedEvent> StockReservationFailedEvent { get; set; }
        public Event<PaymentCompletedEvent> PaymentCompletedEvent { get; set; }
        public Event<PaymentFailedEvent> PaymentFailedEvent { get; set; }

        public OrderStateMachine(ILogger<OrderStateMachine> logger)
        {
            logger.LogInformation("OrderSaga started");
            InstanceState(x => x.CurrentState);

            Event(() => CreateOrderCommand,
                  x => x
                  .CorrelateBy<Guid>(y => y.OrderId, z => z.Message.OrderId)
                  .SelectId(c => c.Message.OrderId));

            Event(() => StockReservedEvent,
                x => x.
                CorrelateById(c => c.Message.OrderId));

            Event(() => StockReservationFailedEvent,
                x => x.
                CorrelateById(c => c.Message.OrderId));

            Event(() => PaymentCompletedEvent,
                x => x.
                CorrelateById(c => c.Message.OrderId));

            Event(() => PaymentFailedEvent,
                x => x.
                CorrelateById(c => c.Message.OrderId));

            Initially(
                When(CreateOrderCommand)
                        .Then(c =>
                        {
                            logger.LogInformation("CREATE ORDER COMMAND RECEIVED | CorrelationId: [{CorrelationId}]", c.Saga.CorrelationId);
                        })
                        .Then(c =>
                        {
                            c.Saga.OrderId = c.Message.OrderId;
                            c.Saga.UserId = c.Message.UserId;
                            c.Saga.CreatedAt = DateTime.Now;
                        })
                        .Publish(c => new OrderCreatedEvent
                        {
                            OrderId = c.Saga.OrderId,
                            UserId = c.Saga.UserId,
                            OrderDetails = c.Message.OrderDetails
                        })
                        .TransitionTo(OrderCreated)
                        .Then(c => logger.LogInformation("ORDER CREATED EVENT PUBLISHED | CorrelationId: [{CorrelationId}]", c.Saga.CorrelationId))
                );

            //During(OrderCreated,
            //    // SUCCESS SCENARIO
            //    When(StockReservedEvent)
            //             .Then(c =>
            //             {
            //                 logger.LogInformation("STOCK RESERVED EVENT RECEIVED | CorrelationId: [{CorrelationId}]", c.Saga.CorrelationId);
            //             })
            //             .TransitionTo(StockReserved)
            //             .Send(new Uri($"queue:{SharedMessage.Constants.CreatePaymentCommandQueue}"), c => new CreatePaymentCommand
            //             {
            //                 OrderId = c.Saga.OrderId,
            //                 UserId = c.Saga.UserId,
            //                 //OrderDetails = c.Saga.OrderDetails,
            //             })
            //             .Then(c =>
            //             {
            //                 logger.LogInformation("CREATE PAYMENT COMMAND SEND | CorrelationId: [{CorrelationId}]", c.Saga.CorrelationId);
            //             }),

            //    // FAILURE SCENARIO
            //             When(StockReservationFailedEvent)
            //            .Then(c =>
            //            {
            //                logger.LogInformation("STOCK RESERVATION FAILED EVENT RECEIVED | CorrelationId: [{CorrelationId}]", c.Saga.CorrelationId);
            //            })
            //            .TransitionTo(StockReservationFailed)
            //            .Publish(c => new OrderFailedEvent
            //            {
            //                OrderId = c.Saga.OrderId,
            //                UserId = c.Saga.UserId,
            //                //OrderDetails = c.Saga.OrderDetails
            //            })
            //            .Then(c =>
            //            {
            //                logger.LogInformation("ORDER FAILED EVENT PUBLISHED | CorrelationId: [{CorrelationId}]", c.Saga.CorrelationId);
            //            })
            //    );

            //During(StockReserved,
            //    // SUCCESS SCENARIO
            //    When(PaymentCompletedEvent)
            //             .Then(c =>
            //             {
            //                 logger.LogInformation("PAYMENT COMPLETED EVENT RECEIVED | CorrelationId: [{CorrelationId}]", c.Saga.CorrelationId);
            //             })
            //             .TransitionTo(PaymentCompleted)
            //             .Publish(c => new OrderCompletedEvent
            //             {
            //                 //OrderDetails = c.Saga.OrderDetails,
            //                 UserId = c.Saga.UserId,
            //                 OrderId = c.Saga.OrderId,
            //             })
            //              .Then(c =>
            //              {
            //                  logger.LogInformation("ORDER COMPLETED EVENT PUBLISHED | CorrelationId: [{CorrelationId}]", c.Saga.CorrelationId);
            //              })
            //             .Finalize(),

            //   // FAILURE SCENARIO
            //      When(PaymentFailedEvent)
            //      .TransitionTo(PaymentFailed)
            //      .Publish(c => new OrderFailedEvent
            //      {
            //          OrderId = c.Saga.OrderId,
            //          UserId = c.Saga.UserId,
            //          //OrderDetails = c.Saga.OrderDetails
            //      })
            //      .Then(c =>
            //      {
            //          logger.LogInformation("ORDER FAILED EVENT PUBLISHED | CorrelationId: [{CorrelationId}]", c.Saga.CorrelationId);
            //      })
            //     .Send(new Uri($"queue:{SharedMessage.Constants.RollbackStockCommandQueue}"), c => new RollbackStockCommand
            //     {
            //         OrderId = c.Saga.OrderId,
            //         UserId = c.Saga.UserId,
            //         //OrderDetails = c.Saga.OrderDetails
            //     })
            //    );
        }
    }
}