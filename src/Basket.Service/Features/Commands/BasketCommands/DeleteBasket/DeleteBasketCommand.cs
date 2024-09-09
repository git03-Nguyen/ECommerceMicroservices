using Contracts.MassTransit.Messages.Events;
using MediatR;

namespace Basket.Service.Features.Commands.BasketCommands.DeleteBasket;

public class DeleteBasketCommand : IRequest
 {
     public AccountDeleted Payload { get; set; }
     
        public DeleteBasketCommand(AccountDeleted payload)
        {
            Payload = payload;
        }
 }