using System.Linq;
using System.Threading.Tasks;
using Spirebyte.Services.Activities.Application.Activities.Services.Interfaces;
using Spirebyte.Services.Activities.Core.Entities;

namespace Spirebyte.Services.Activities.Infrastructure.Services;

public class HubService : IHubService
{
    private readonly IHubWrapper _hubContextWrapper;

    public HubService(IHubWrapper hubContextWrapper)
    {
        _hubContextWrapper = hubContextWrapper;
    }

    public async Task PublishActivityAsync(Activity activity)
    {
        if (activity.UsersToNotify.Any())
        {
            foreach (var userId in activity.UsersToNotify)
                await _hubContextWrapper.PublishToUserAsync(userId.ToString(), "new_activity", activity);

            await _hubContextWrapper.PublishToUserAsync(activity.UserId.ToString(), "new_activity", activity);
        }
        else
        {
            await _hubContextWrapper.PublishToProjectAsync(activity.ProjectId, "new_activity", activity);
        }
    }
}