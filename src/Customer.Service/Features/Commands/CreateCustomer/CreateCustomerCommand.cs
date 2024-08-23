using MediatR;
using Shared.Abstractions.Messaging;

namespace Customer.Service.Features.Commands.CreateCustomer;

public sealed record CreateCustomerCommand(Models.Customer Customer) : ICommand<Models.Customer>;
// Nguy hiem
