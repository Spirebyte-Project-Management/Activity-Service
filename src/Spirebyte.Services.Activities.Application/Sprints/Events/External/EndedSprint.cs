using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Spirebyte.Services.Activities.Application.Sprints.Events.External;

[Message("sprints")]
internal record EndedSprint(string SprintId) : IEvent;