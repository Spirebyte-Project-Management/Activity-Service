using System;
using Spirebyte.Services.Activities.Core.Exceptions.Base;

namespace Spirebyte.Services.Activities.Core.Exceptions;

public class InvalidUserIdException : DomainException
{
    public InvalidUserIdException(Guid userId) : base($"Invalid userId: {userId}.")
    {
    }

    public override string Code { get; } = "invalid_user_id";
}