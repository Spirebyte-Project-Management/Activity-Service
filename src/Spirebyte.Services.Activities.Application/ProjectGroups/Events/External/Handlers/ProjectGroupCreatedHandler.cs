using System;
using System.Threading;
using System.Threading.Tasks;
using Spirebyte.Framework.Contexts;
using Spirebyte.Framework.Shared.Handlers;
using Spirebyte.Services.Activities.Application.Activities.Services.Interfaces;
using Spirebyte.Services.Activities.Core.Entities;
using Spirebyte.Services.Activities.Core.Enums;
using Spirebyte.Services.Activities.Core.Repositories;

namespace Spirebyte.Services.Activities.Application.ProjectGroups.Events.External.Handlers;

public class ProjectGroupCreatedHandler : IEventHandler<ProjectGroupCreated>
{
    private readonly IActivityRepository _activityRepository;
    private readonly IContextAccessor _contextAccessor;
    private readonly IHubService _hubService;

    public ProjectGroupCreatedHandler(IActivityRepository activityRepository, IHubService hubService,
        IContextAccessor contextAccessor)
    {
        _activityRepository = activityRepository;
        _hubService = hubService;
        _contextAccessor = contextAccessor;
    }

    public async Task HandleAsync(ProjectGroupCreated @event, CancellationToken cancellationToken = default)
    {
        var activity = new Activity(Guid.NewGuid(), _contextAccessor.Context.GetUserId(), Array.Empty<Guid>(), @event.ProjectId,
            ActivityType.ProjectGroupCreated, new[] { @event as object }, DateTime.Now);
        await _activityRepository.AddAsync(activity);

        await _hubService.PublishActivityAsync(activity);
    }
}