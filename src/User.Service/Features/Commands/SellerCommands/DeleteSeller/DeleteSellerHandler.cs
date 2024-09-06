using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Service.Repositories;

namespace User.Service.Features.Commands.SellerCommands.DeleteSeller;

public class DeleteSellerHandler : IRequestHandler<DeleteSellerCommand>
{
    private readonly ILogger<DeleteSellerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSellerHandler(IUnitOfWork unitOfWork, ILogger<DeleteSellerHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(DeleteSellerCommand request, CancellationToken cancellationToken)
    {
        var seller = await _unitOfWork.SellerRepository.GetByCondition(x => x.AccountId == request.AccountId)
            .FirstOrDefaultAsync(cancellationToken);
        if (seller != null)
        {
            _unitOfWork.SellerRepository.Remove(seller);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Deleting seller with account id: {AccountId}", request.AccountId);
        }
    }
}