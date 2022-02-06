using System;
using Spirebyte.Services.Activities.Core.Enums;
using Spirebyte.Services.Activities.Core.Exceptions;

namespace Spirebyte.Services.Activities.Core.Entities;

public class Activity
{
    public Activity(Guid id, Guid userId, Guid[] usersToNotify, string projectId, ActivityType type,
        object[] dataObjects, DateTime createdAt)
    {
        if (id == Guid.Empty) throw new InvalidIdException(id);

        if (string.IsNullOrEmpty(projectId)) throw new InvalidProjectIdException(projectId);

        if (userId == Guid.Empty) throw new InvalidUserIdException(userId);

        Id = id;
        UserId = userId;
        UsersToNotify = usersToNotify;
        ProjectId = projectId;
        Type = type;
        DataObjects = dataObjects;
        CreatedAt = createdAt == DateTime.MinValue ? DateTime.Now : createdAt;
    }

    public Guid Id { get; }
    public Guid UserId { get; }
    public Guid[] UsersToNotify { get; }
    public string ProjectId { get; }

    public ActivityType Type { get; }
    public object[] DataObjects { get; }

    public DateTime CreatedAt { get; }
}