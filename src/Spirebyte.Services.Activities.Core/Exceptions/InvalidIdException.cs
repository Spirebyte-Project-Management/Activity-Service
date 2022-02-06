using System;
using Spirebyte.Services.Activities.Core.Exceptions.Base;

namespace Spirebyte.Services.Activities.Core.Exceptions;

public class InvalidIdException : DomainException
{
    public InvalidIdException(Guid id) : base($"Invalid id: {id}.")
    {
    }

    public override string Code { get; } = "invalid_id";
}