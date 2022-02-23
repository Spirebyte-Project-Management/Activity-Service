using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Spirebyte.Services.Activities.Application.Sprints.Events.External;

[Message("sprints")]
public record StartedSprint(string SprintId, string ProjectId) : IEvent;