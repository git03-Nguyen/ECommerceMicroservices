using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Service.Repositories;

namespace User.Service.Features.Commands.CustomerCommands.DeleteCustomer;

public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand>
{
    private readonly ILogger<DeleteCustomerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCustomerHandler(IUnitOfWork unitOfWork, ILogger<DeleteCustomerHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.CustomerRepository.GetByCondition(x => x.AccountId == request.AccountId)
            .FirstOrDefaultAsync(cancellationToken);
        if (customer != null)
        {
            _unitOfWork.CustomerRepository.Remove(customer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Deleting customer with account id: {AccountId}", request.AccountId);
            // TODO: Publish event
        }
    }
}