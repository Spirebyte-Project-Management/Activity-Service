using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Spirebyte.Framework.Contexts;

namespace Spirebyte.Services.Activities.Infrastructure.Hubs;

public class SpirebyteHub : Hub
{
    private readonly IContextAccessor _contextAccessor;

    public SpirebyteHub(IContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public async Task JoinProjectActivityStream(string projectId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, projectId.ToProjectGroup());
    }

    public async Task LeaveProjectActivityStream(string projectId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, projectId.ToProjectGroup());
    }

    public async Task InitializeAsync()
    {
        if (string.IsNullOrWhiteSpace(_contextAccessor.Context?.UserId)) await DisconnectAsync();
        try
        {
            var group = Guid.Parse(_contextAccessor.Context?.UserId).ToUserGroup();
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
            await ConnectAsync();
        }
        catch
        {
            await DisconnectAsync();
        }
    }

    private async Task ConnectAsync()
    {
        await Clients.Client(Context.ConnectionId).SendAsync("connected");
    }

    private async Task DisconnectAsync()
    {
        await Clients.Client(Context.ConnectionId).SendAsync("disconnected");
    }
}