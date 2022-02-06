using Spirebyte.Services.Activities.Application.Contexts;

namespace Spirebyte.Services.Activities.Infrastructure.Contexts;

public interface IAppContextFactory
{
    IAppContext Create();
}