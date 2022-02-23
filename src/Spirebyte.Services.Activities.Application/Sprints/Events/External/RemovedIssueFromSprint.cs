using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Spirebyte.Services.Activities.Application.Sprints.Events.External;

[Message("sprints")]
public record RemovedIssueFromSprint(string SprintId, string ProjectId, string IssueId) : IEvent;