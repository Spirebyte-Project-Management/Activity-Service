using System;
using Convey.CQRS.Queries;
using Spirebyte.Services.Activities.Application.Activities.DTO;

namespace Spirebyte.Services.Activities.Application.Activities.Queries;

public class BrowseActivities : PagedQueryBase, IQuery<PagedResult<ActivityDto>>
{
    public string? ProjectId { get; set; }
    public Guid? UserId { get; set; }
}