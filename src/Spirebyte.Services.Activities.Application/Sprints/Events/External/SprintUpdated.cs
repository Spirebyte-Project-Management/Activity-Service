using System;
using Convey.CQRS.Events;

namespace Spirebyte.Services.Activities.Application.Sprints.Events.External;

[Contract]
internal record SprintUpdated(string SprintId, DateTime StartedAt, DateTime EndedAt) : IEvent;