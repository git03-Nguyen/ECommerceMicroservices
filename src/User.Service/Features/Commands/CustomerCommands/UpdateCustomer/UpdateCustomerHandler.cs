using MediatR;
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
        var customer = _unitOfWork.CustomerRepository.GetByCondition(x => x.Email == request.Payload.Email).FirstOrDefault();
        if (customer == null) throw new Exception("Customer not found");
        
        customer.Username = String.IsNullOrWhiteSpace(request.Payload.Username) ? customer.Username : request.Payload.Username;
        customer.FullName = String.IsNullOrWhiteSpace(request.Payload.FullName) ? customer.FullName : request.Payload.FullName;
        customer.PhoneNumber = String.IsNullOrWhiteSpace(request.Payload.PhoneNumber) ? customer.PhoneNumber : request.Payload.PhoneNumber;
        customer.Address = String.IsNullOrWhiteSpace(request.Payload.Address) ? customer.Address : request.Payload.Address;
        customer.PaymentMethod = String.IsNullOrWhiteSpace(request.Payload.PaymentMethod) ? customer.PaymentMethod : request.Payload.PaymentMethod;
        
        _unitOfWork.CustomerRepository.Update(customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("UpdateCustomerHandler.Handle: {0} - {1} - {2} - {3} - {4}", customer.Email, customer.Username, customer.FullName, customer.PhoneNumber, customer.Address);
        // TODO: Produce a message: CustomerUpdatedEvent
        
        return new UpdateCustomerResponse(customer);



    }
}