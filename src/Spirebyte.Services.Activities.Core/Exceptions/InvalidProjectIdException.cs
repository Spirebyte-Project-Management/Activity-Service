using Spirebyte.Services.Activities.Core.Exceptions.Base;

namespace Spirebyte.Services.Activities.Core.Exceptions;

public class InvalidProjectIdException : DomainException
{
    public InvalidProjectIdException(string projectId) : base($"Invalid projectId: {projectId}.")
    {
    }

    public override string Code { get; } = "invalid_project_id";
}