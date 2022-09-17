using Spirebyte.Framework.Shared.Abstractions;
using Spirebyte.Framework.Shared.Attributes;

namespace Spirebyte.Services.Activities.Application.Sprints.Events.External;

[Message("sprints", "added_issue_to_sprint", "activities.added_issue_to_sprint")]
public record AddedIssueToSprint(string SprintId, string ProjectId, string IssueId) : IEvent;