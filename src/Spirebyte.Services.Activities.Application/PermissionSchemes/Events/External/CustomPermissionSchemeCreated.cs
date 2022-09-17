using System;
using System.Collections.Generic;
using Spirebyte.Framework.Shared.Abstractions;
using Spirebyte.Framework.Shared.Attributes;
using Spirebyte.Services.Activities.Application.PermissionSchemes.Events.External.Objects;

namespace Spirebyte.Services.Activities.Application.PermissionSchemes.Events.External;

[Message("projects", "custom_permissions_scheme_created", "activities.custom_permissions_scheme_created")]
public class CustomPermissionSchemeCreated : IEvent
{
    public string ProjectId { get; set; }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IEnumerable<Permission> Permissions { get; set; }
}