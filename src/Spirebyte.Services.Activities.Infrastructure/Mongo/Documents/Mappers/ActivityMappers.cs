using System.Linq;
using Spirebyte.Services.Activities.Application.Activities.DTO;
using Spirebyte.Services.Activities.Core.Entities;

namespace Spirebyte.Services.Activities.Infrastructure.Mongo.Documents.Mappers;

internal static class ActivityMappers
{
    public static Activity AsEntity(this ActivityDocument document)
    {
        return new Activity(document.Id, document.UserId, document.UsersToNotify, document.ProjectId,
            document.Type, document.DataObjects, document.CreatedAt);
    }

    public static ActivityDocument AsDocument(this Activity entity)
    {
        return new ActivityDocument
        {
            Id = entity.Id,
            UserId = entity.UserId,
            UsersToNotify = entity.UsersToNotify,
            ProjectId = entity.ProjectId,
            Type = entity.Type,
            DataObjects = entity.DataObjects,
            CreatedAt = entity.CreatedAt
        };
    }

    public static ActivityDto AsDto(this ActivityDocument document)
    {
        return new ActivityDto
        {
            Id = document.Id,
            UserId = document.UserId,
            UsersToNotify = document.UsersToNotify,
            ProjectId = document.ProjectId,
            Type = document.Type,
            DataObjects = document.DataObjects,
            CreatedAt = document.CreatedAt
        };
    }
}