using System;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Spirebyte.Services.Activities.Application.Projects.Events.External;

[Message("projects")]
public class UserInvitedToProject : IEvent
{
    public string Id { get; set; }
    public string ProjectTitle { get; set; }
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string EmailAddress { get; set; }
}