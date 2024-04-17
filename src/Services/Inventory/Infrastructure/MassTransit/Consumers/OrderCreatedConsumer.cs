using Inventory.Application.Data;
using Inventory.Domain.Inventory;
using MassTransit;
using Microsoft.Extensions.Logging;
using SharedMessage.Events;

namespace Inventory.Infrastructure.MassTransit.Consumers
{
    internal class OrderCreatedConsumer(ILogger<OrderCreatedConsumer> logger, IInventoryRepository inventoryRepository, IUnitOfWork unitOfWork) : IConsumer<OrderCreatedEvent>
    {
        private readonly ILogger<OrderCreatedConsumer> _logger = logger;
        private readonly IInventoryRepository _inventoryRepository = inventoryRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            _logger.LogInformation("ORDER CREATED EVENT RECEIVED ON CONSUMER with OrderId: [{orderId}]", context.Message.OrderId);

            var orderCreatedEvent = context.Message;
            await Task.CompletedTask;

            foreach (var orderDetail in orderCreatedEvent.OrderDetails)
            {
                var inventory = await _inventoryRepository.GetByProductIdAsync(orderDetail.ProductId);

                if (inventory != null)
                {
                    if (inventory.Quantity - orderDetail.Quantity >= 0)
                    {
                        _logger.LogInformation("[{QUANTITY}] STOCK RESERVED FOR PRODUCT: [{PRODUCTID}] with OrderId: [{orderId}]", orderDetail.Quantity, orderDetail.ProductId, context.Message.OrderId);

                        // update stock
                        inventory.ReduceStock(orderDetail.Quantity);

                        await _inventoryRepository.UpdateAsync(inventory);

                        await context.Publish(new StockReservedEvent
                        {
                            OrderDetails = context.Message.OrderDetails,
                            OrderId = context.Message.OrderId,
                            UserId = context.Message.UserId
                        });
                    }
                    else
                    {
                        _logger.LogInformation("INSUFFICIENT STOCK FOR PRODUCT: [{PRODUCTID}] with OrderId: [{orderId}]", orderDetail.ProductId, context.Message.OrderId);

                        await context.Publish(new StockReservationFailedEvent
                        {
                            OrderDetails = context.Message.OrderDetails,
                            OrderId = context.Message.OrderId,
                            UserId = context.Message.UserId
                        });
                    }

                    // call save changes
                    await _unitOfWork.SaveChangesAsync();
                }
            }
        }
    }
}