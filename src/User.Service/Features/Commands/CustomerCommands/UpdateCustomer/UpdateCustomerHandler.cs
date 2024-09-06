using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Service.Repositories;

namespace User.Service.Features.Commands.CustomerCommands.UpdateCustomer;

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, UpdateCustomerResponse>
{
    private readonly ILogger<UpdateCustomerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCustomerHandler(ILogger<UpdateCustomerHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateCustomerResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.CustomerRepository
            .GetByCondition(x => x.AccountId == request.Payload.AccountId).FirstOrDefaultAsync(cancellationToken);
        if (customer == null) throw new Exception("Customer not found");

        customer.Account.Email = string.IsNullOrWhiteSpace(request.Payload.Email)
            ? customer.Account.Email
            : request.Payload.Email;
        customer.Account.UserName = string.IsNullOrWhiteSpace(request.Payload.UserName)
            ? customer.Account.UserName
            : request.Payload.UserName;
        customer.FullName = string.IsNullOrWhiteSpace(request.Payload.FullName)
            ? customer.FullName
            : request.Payload.FullName;
        customer.PhoneNumber = string.IsNullOrWhiteSpace(request.Payload.PhoneNumber)
            ? customer.PhoneNumber
            : request.Payload.PhoneNumber;
        customer.Address = string.IsNullOrWhiteSpace(request.Payload.Address)
            ? customer.Address
            : request.Payload.Address;
        customer.PaymentMethod = string.IsNullOrWhiteSpace(request.Payload.PaymentMethod)
            ? customer.PaymentMethod
            : request.Payload.PaymentMethod;

        _unitOfWork.CustomerRepository.Update(customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("UpdateSellerHandler.Handle: {id} - {0} - {1} - {2} - {3} - {4}", customer.AccountId,
            customer.Account.Email, customer.Account.UserName, customer.FullName, customer.PhoneNumber,
            customer.Address);
        // TODO: Produce a message: CustomerUpdatedEvent

        return new UpdateCustomerResponse(customer);
    }
}