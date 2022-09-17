using Spirebyte.Framework.Shared.Abstractions;
using Spirebyte.Framework.Shared.Attributes;

namespace Spirebyte.Services.Activities.Application.Sprints.Events.External;

[Message("sprints", "removed_issue_from_sprint", "activities.removed_issue_from_sprint")]
public record RemovedIssueFromSprint(string SprintId, string ProjectId, string IssueId) : IEvent;