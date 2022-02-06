using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Spirebyte.Services.Activities.Application.Activities.Services.Interfaces;
using Spirebyte.Services.Activities.Infrastructure.Hubs;

namespace Spirebyte.Services.Activities.Infrastructure.Services;

public class HubWrapper : IHubWrapper
{
    private readonly IHubContext<SpirebyteHub> _hubContext;

    public HubWrapper(IHubContext<SpirebyteHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task PublishToUserAsync(string userId, string message, object data)
    {
        await _hubContext.Clients.Group(userId.ToUserGroup()).SendAsync(message, data);
    }

    public async Task PublishToProjectAsync(string projectId, string message, object data)
    {
        await _hubContext.Clients.Group(projectId.ToProjectGroup()).SendAsync(message, data);
    }

    public async Task PublishToAllAsync(string message, object data)
    {
        await _hubContext.Clients.All.SendAsync(message, data);
    }
}