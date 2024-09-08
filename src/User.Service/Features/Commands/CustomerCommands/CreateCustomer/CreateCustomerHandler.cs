using MediatR;
using User.Service.Data.Models;
using User.Service.Repositories;

namespace User.Service.Features.Commands.CustomerCommands.CreateCustomer;

public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, bool>
{
    private readonly ILogger<CreateCustomerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerHandler(ILogger<CreateCustomerHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer
        {
            AccountId = request.Payload.Id,
            Account = new Account
            {
                AccountId = request.Payload.Id,
                Email = request.Payload.Email,
                UserName = request.Payload.UserName
            },
            FullName = "Khách hàng " + request.Payload.UserName
        };

        await _unitOfWork.CustomerRepository.AddAsync(customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}