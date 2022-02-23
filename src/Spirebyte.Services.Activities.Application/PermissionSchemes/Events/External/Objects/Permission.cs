using System.Collections.Generic;
using Spirebyte.Services.Projects.Core.Entities;

namespace Spirebyte.Services.Activities.Application.PermissionSchemes.Events.External.Objects;

public class Permission
{
    public string Key { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string PermissionGroup { get; set; }
    public IEnumerable<Grant> Grants { get; set; }
}