using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using Spirebyte.Services.Activities.Application.Activities.DTO;
using Spirebyte.Services.Activities.Application.Activities.Queries;
using Spirebyte.Services.Activities.Infrastructure.Mongo.Documents;
using Spirebyte.Services.Activities.Infrastructure.Mongo.Documents.Mappers;

namespace Spirebyte.Services.Activities.Infrastructure.Mongo.Queries.Handlers;

internal sealed class BrowseActivitiesHandler : IQueryHandler<BrowseActivities, PagedResult<ActivityDto>>
{
    private readonly IMongoRepository<ActivityDocument, Guid> _activitiesRepository;

    public BrowseActivitiesHandler(IMongoRepository<ActivityDocument, Guid> activitiesRepository)
    {
        _activitiesRepository = activitiesRepository;
    }

    public async Task<PagedResult<ActivityDto>> HandleAsync(BrowseActivities query,
        CancellationToken cancellationToken = default)
    {
        Expression<Func<ActivityDocument, bool>> expression = x => true;

        if (!string.IsNullOrWhiteSpace(query.ProjectId))
            expression = expression.And(x => x.ProjectId == query.ProjectId);

        if (query.UserId is not null) expression = expression.And(x => x.ProjectId == query.ProjectId);

        var result = await _activitiesRepository.BrowseAsync(expression, query);
        var activities = result.Items.Select(x => x.AsDto()).ToList();

        return PagedResult<ActivityDto>.From(result, activities);
    }
}