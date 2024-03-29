﻿using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Spirebyte.Framework.DAL.MongoDb;
using Spirebyte.Framework.DAL.MongoDb.Interfaces;
using Spirebyte.Framework.Shared.Handlers;
using Spirebyte.Framework.Shared.Pagination;
using Spirebyte.Services.Activities.Application.Activities.DTO;
using Spirebyte.Services.Activities.Application.Activities.Queries;
using Spirebyte.Services.Activities.Infrastructure.Mongo.Documents;
using Spirebyte.Services.Activities.Infrastructure.Mongo.Documents.Mappers;

namespace Spirebyte.Services.Activities.Infrastructure.Mongo.Queries.Handlers;

internal sealed class BrowseActivitiesHandler : IQueryHandler<BrowseActivities, Paged<ActivityDto>>
{
    private readonly IMongoRepository<ActivityDocument, Guid> _activitiesRepository;

    public BrowseActivitiesHandler(IMongoRepository<ActivityDocument, Guid> activitiesRepository)
    {
        _activitiesRepository = activitiesRepository;
    }

    public async Task<Paged<ActivityDto>> HandleAsync(BrowseActivities query,
        CancellationToken cancellationToken = default)
    {
        var pagedActivities = await _activitiesRepository.BrowseAsync(query);

        return pagedActivities.Map(c => c.AsDto());
    }
}