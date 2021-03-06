using System;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Spirebyte.Services.Activities.Application.Projects.Events.External;

[Message("projects")]
public class ProjectLeft : IEvent
{
    public string Id { get; set; }

    public Guid UserId { get; set; }
}