using Contracts.MassTransit.Messages.Events;
using MediatR;

namespace Basket.Service.Features.Commands.BasketCommands.DeleteBasket;

public class DeleteBasketCommand : IRequest
 {
     public IAccountDeleted Payload { get; set; }
     
        public DeleteBasketCommand(IAccountDeleted payload)
        {
            Payload = payload;
        }
 }