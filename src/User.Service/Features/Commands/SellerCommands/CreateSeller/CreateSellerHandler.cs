using MediatR;
using User.Service.Data.Models;
using User.Service.Repositories;

namespace User.Service.Features.Commands.SellerCommands.CreateSeller;

public class CreateSellerHandler : IRequestHandler<CreateSellerCommand, bool>
{
    private readonly ILogger<CreateSellerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSellerHandler(ILogger<CreateSellerHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(CreateSellerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Create the user
            var seller = new Seller
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

            // Save the user to the database
            await _unitOfWork.SellerRepository.AddAsync(seller);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in AccountCreatedConsumer");
            // Rollback the transaction
            throw;
        }
    }
}