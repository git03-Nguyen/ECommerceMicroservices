namespace Customer.Service.Features.Commands.CreateCustomer;

public sealed record CreateCustomerCommand(Models.Customer Customer) : Abstractions.ICommand<Models.Customer>;
// Nguy hiem