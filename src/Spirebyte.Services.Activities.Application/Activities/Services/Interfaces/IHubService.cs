using System.Threading.Tasks;
using Spirebyte.Services.Activities.Core.Entities;

namespace Spirebyte.Services.Activities.Application.Activities.Services.Interfaces;

public interface IHubService
{
    Task PublishActivityAsync(Activity activity);
}