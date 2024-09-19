using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Auth.Service.SignalRWebpack.Hubs;

public class StronglyTypedChatHub : Hub<IChatClient>
{
    [HubMethodName("SendMessage")]
    public async Task SendMessage(string username, string message)
        => await Clients.All.ReceiveMessage(username, message);
    
    public async Task SendMessageToCaller(string user, string message)
        => await Clients.Caller.ReceiveMessage(user, message);

    public async Task SendMessageToGroup(string user, string message)
        => await Clients.Group("SignalR Users").ReceiveMessage(user, message);

    public override async Task OnConnectedAsync()
    {
        Console.WriteLine("Client connected" + Context.ConnectionId);
        await base.OnConnectedAsync();
    }
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine("Client disconnected" + Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }
    
    public Task ThrowException()
    {
        throw new HubException("This method is not supported");
    }
}

public interface IChatClient
{
    Task ReceiveMessage(string username, string message);
}