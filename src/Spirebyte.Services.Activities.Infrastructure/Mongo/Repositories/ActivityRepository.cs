using System;
using System.Threading.Tasks;
using Convey.Persistence.MongoDB;
using Spirebyte.Services.Activities.Core.Entities;
using Spirebyte.Services.Activities.Core.Repositories;
using Spirebyte.Services.Activities.Infrastructure.Mongo.Documents;
using Spirebyte.Services.Activities.Infrastructure.Mongo.Documents.Mappers;

namespace Spirebyte.Services.Activities.Infrastructure.Mongo.Repositories;

internal class ActivityRepository : IActivityRepository
{
    private readonly IMongoRepository<ActivityDocument, Guid> _repository;

    public ActivityRepository(IMongoRepository<ActivityDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<Activity> GetAsync(Guid id)
    {
        var activity = await _repository.GetAsync(id);
        return activity.AsEntity();
    }

    public async Task AddAsync(Activity activity)
    {
        await _repository.AddAsync(activity.AsDocument());
    }

    public async Task UpdateAsync(Activity activity)
    {
        await _repository.UpdateAsync(activity.AsDocument());
    }
}