using System;
using Convey.Types;
using Spirebyte.Services.Activities.Core.Enums;

namespace Spirebyte.Services.Activities.Infrastructure.Mongo.Documents;

public class ActivityDocument : IIdentifiable<Guid>
{
    public Guid UserId { get; set; }
    public Guid[] UsersToNotify { get; set; }
    public string ProjectId { get; set; }

    public ActivityType Type { get; set; }
    public object[] DataObjects { get; set; }

    public DateTime CreatedAt { get; set; }
    public Guid Id { get; set; }
}