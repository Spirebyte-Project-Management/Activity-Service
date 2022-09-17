using System;
using Spirebyte.Framework.Shared.Abstractions;
using Spirebyte.Framework.Shared.Attributes;

namespace Spirebyte.Services.Activities.Application.Projects.Events.External;

[Message("projects", "user_invited_to_project", "activities.user_invited_to_project")]
public class UserInvitedToProject : IEvent
{
    public string Id { get; set; }
    public string ProjectTitle { get; set; }
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string EmailAddress { get; set; }
}