using System;
using Spirebyte.Framework.Shared.Pagination;
using Spirebyte.Services.Activities.Application.Activities.DTO;

namespace Spirebyte.Services.Activities.Application.Activities.Queries;

public class BrowseActivities : PagedQuery<ActivityDto>
{
    public string? ProjectId { get; set; }
    public Guid? UserId { get; set; }
}