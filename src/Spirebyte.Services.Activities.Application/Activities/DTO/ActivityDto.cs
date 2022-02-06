using System;
using Spirebyte.Services.Activities.Core.Enums;

namespace Spirebyte.Services.Activities.Application.Activities.DTO;

public class ActivityDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid[] UsersToNotify { get; set; }
    public string ProjectId { get; set; }

    public ActivityType Type { get; set; }
    public object[] DataObjects { get; set; }

    public DateTime CreatedAt { get; set; }
}