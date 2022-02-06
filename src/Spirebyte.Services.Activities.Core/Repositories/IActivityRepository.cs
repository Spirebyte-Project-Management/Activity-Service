using System;
using System.Threading.Tasks;
using Spirebyte.Services.Activities.Core.Entities;

namespace Spirebyte.Services.Activities.Core.Repositories;

public interface IActivityRepository
{
    Task<Activity> GetAsync(Guid id);
    Task AddAsync(Activity activity);
    Task UpdateAsync(Activity activity);
}