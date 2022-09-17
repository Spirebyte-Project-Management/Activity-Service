using System;
using Spirebyte.Framework.Shared.Exceptions;

namespace Spirebyte.Services.Activities.Core.Exceptions;

public class InvalidIdException : DomainException
{
    public InvalidIdException(Guid id) : base($"Invalid id: {id}.")
    {
    }

    public string Code { get; } = "invalid_id";
}