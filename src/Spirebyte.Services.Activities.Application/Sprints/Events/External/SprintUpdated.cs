﻿using System;
using System.Collections.Generic;
using Spirebyte.Framework.Shared.Abstractions;
using Spirebyte.Framework.Shared.Attributes;
using Spirebyte.Shared.Changes.ValueObjects;

namespace Spirebyte.Services.Activities.Application.Sprints.Events.External;

[Message("sprints", "sprint_updated", "activities.sprint_updated")]
public class SprintUpdated : IEvent
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<string> IssueIds { get; set; }
    public string ProjectId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime EndedAt { get; set; }
    public int RemainingStoryPoints { get; set; }
    public int TotalStoryPoints { get; set; }

    public Change[] Changes { get; set; }
}