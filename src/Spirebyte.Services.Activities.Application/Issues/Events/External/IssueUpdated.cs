using System;
using System.Collections.Generic;
using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Spirebyte.Services.Activities.Core.Enums;
using Spirebyte.Shared.Changes.ValueObjects;

namespace Spirebyte.Services.Activities.Application.Issues.Events.External;

[Message("issues")]
public class IssueUpdated : IEvent
{
    public string Id { get; set; }
    public IssueType Type { get; set; }
    public IssueStatus Status { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int StoryPoints { get; set; }

    public string ProjectId { get; set; }
    public string EpicId { get; set; }
    public string SprintId { get; set; }
    public IEnumerable<Guid> Assignees { get; set; }
    public IEnumerable<Guid> LinkedIssues { get; set; }

    public DateTime CreatedAt { get; set; }

    public Change[] Changes { get; set; }
}