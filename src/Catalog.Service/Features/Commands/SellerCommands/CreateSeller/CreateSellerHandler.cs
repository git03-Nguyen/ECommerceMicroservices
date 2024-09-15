using Catalog.Service.Data.Models;
using Catalog.Service.Repositories;
using MediatR;

namespace Catalog.Service.Features.Commands.SellerCommands.CreateSeller;

public class CreateSellerHandler : IRequestHandler<CreateSellerCommand>
{
    private readonly ILogger<CreateSellerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSellerHandler(ILogger<CreateSellerHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateSellerCommand request, CancellationToken cancellationToken)
    {
        var seller = new Seller
        {
            SellerId = request.Payload.Id,
            Name = request.Payload.FullName
        };

        await _unitOfWork.SellerRepository.AddAsync(seller);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}