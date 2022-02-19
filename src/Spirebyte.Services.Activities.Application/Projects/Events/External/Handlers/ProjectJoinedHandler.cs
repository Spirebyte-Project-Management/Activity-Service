using System;
using System.Threading;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using Spirebyte.Services.Activities.Application.Activities.Services.Interfaces;
using Spirebyte.Services.Activities.Core.Entities;
using Spirebyte.Services.Activities.Core.Enums;
using Spirebyte.Services.Activities.Core.Repositories;
using Spirebyte.Shared.Contexts.Interfaces;

namespace Spirebyte.Services.Activities.Application.Projects.Events.External.Handlers;

public class ProjectJoinedHandler : IEventHandler<ProjectJoined>
{
    private readonly IActivityRepository _activityRepository;
    private readonly IAppContext _appContext;
    private readonly IHubService _hubService;

    public ProjectJoinedHandler(IActivityRepository activityRepository, IHubService hubService, IAppContext appContext)
    {
        _activityRepository = activityRepository;
        _hubService = hubService;
        _appContext = appContext;
    }

    public async Task HandleAsync(ProjectJoined @event, CancellationToken cancellationToken = default)
    {
        var activity = new Activity(Guid.NewGuid(), _appContext.Identity.Id, Array.Empty<Guid>(), @event.ProjectId,
            ActivityType.UsersJoinedProject, new[] { @event as object }, DateTime.Now);
        await _activityRepository.AddAsync(activity);

        await _hubService.PublishActivityAsync(activity);
    }
}