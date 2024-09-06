// using Auth.Service.Features.Commands.UserCommands.RollbackSignUp;
// using Contracts.MassTransit.Events;
// using MassTransit;
// using MediatR;
//
// namespace Auth.Service.Consumers;
//
// public class NewAccountCreatedFaultConsumer : IConsumer<Fault<NewAccountCreated>>
// {
//     private readonly IMediator _mediator;
//
//     public NewAccountCreatedFaultConsumer(IMediator mediator)
//     {
//         _mediator = mediator;
//     }
//
//     public async Task Consume(ConsumeContext<Fault<NewAccountCreated>> context)
//     {
//         var newAccountCreated = context.Message.Message;
//         var fault = context.Message;
//
//         var rollbackSignUpCommand = new RollBackSignUpCommand(newAccountCreated);
//         await _mediator.Send(rollbackSignUpCommand);
//     }
// }