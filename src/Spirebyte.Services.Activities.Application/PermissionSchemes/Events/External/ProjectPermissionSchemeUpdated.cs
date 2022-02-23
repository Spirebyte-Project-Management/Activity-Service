using System;
using System.Collections.Generic;
using Convey.CQRS.Events;
using Spirebyte.Services.Activities.Application.PermissionSchemes.Events.External.Objects;
using Spirebyte.Services.Activities.Core.ValueObjects;

namespace Spirebyte.Services.Activities.Application.PermissionSchemes.Events.External;

[Contract]
public class ProjectPermissionSchemeUpdated : IEvent
{
    public string ProjectId { get; set; }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IEnumerable<Permission> Permissions { get; set; }
    
    public Change[] Changes { get; set; }
}