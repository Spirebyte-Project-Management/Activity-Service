using Convey.CQRS.Events;

namespace Spirebyte.Services.Activities.Application.Sprints.Events.External;

[Contract]
internal record StartedSprint(string SprintId) : IEvent;