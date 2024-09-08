using Basket.Service.Repositories;
using Basket.Service.Services.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Basket.Service.Features.Commands.BasketCommands.CreateBasket;

public class CreateBasketHandler : IRequestHandler<CreateBasketCommand>
{
    private readonly ILogger<CreateBasketHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBasketHandler(ILogger<CreateBasketHandler> logger, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(CreateBasketCommand request, CancellationToken cancellationToken)
    {
        var oldBasket = await _unitOfWork.BasketRepository.GetByCondition(x => x.AccountId == request.Payload.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (oldBasket != null) return;

        var newBasket = new Data.Models.Basket
        {
            AccountId = request.Payload.Id
        };

        await _unitOfWork.BasketRepository.AddAsync(newBasket);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation($"Basket created for account {request.Payload.Id}");
    }
}