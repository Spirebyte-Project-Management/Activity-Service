using Spirebyte.Framework.Shared.Abstractions;
using Spirebyte.Framework.Shared.Attributes;

namespace Spirebyte.Services.Activities.Application.Sprints.Events.External;

[Message("sprints", "ended_sprint", "activities.ended_sprint")]
public record EndedSprint(string SprintId, string ProjectId) : IEvent;