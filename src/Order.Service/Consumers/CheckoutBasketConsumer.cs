using Contracts.Domain;
using Contracts.Masstransit.Queues;
using MassTransit;
using Newtonsoft.Json;
using Order.Service.Data.Models;

namespace Order.Service.Consumers;

public class CheckoutBasketConsumer : IConsumer<CheckoutBasket>
{
    public async Task Consume(ConsumeContext<CheckoutBasket> context)
    {
        var basket = context.Message;
        Console.WriteLine($"Basket received: {basket.BasketId}");
    }
}