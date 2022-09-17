using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Spirebyte.Framework.Messaging;
using Spirebyte.Framework.Messaging.Subscribers;
using Spirebyte.Services.Activities.Application.Issues.Events.External;
using Spirebyte.Services.Activities.Application.PermissionSchemes.Events.External;
using Spirebyte.Services.Activities.Application.ProjectGroups.Events.External;
using Spirebyte.Services.Activities.Application.Projects.Events.External;
using Spirebyte.Services.Activities.Application.Sprints.Events.External;

namespace Spirebyte.Services.Activities.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }

    public static IApplicationBuilder UseApplication(this IApplicationBuilder app)
    {
        app.Subscribe()
            .SubscribeToExternalProjectEvents()
            .SubscribeToExternalProjectGroupEvents()
            .SubscribeToExternalPermissionSchemeEvents()
            .SubscribeToExternalIssueEvents()
            .SubscribeToExternalSprintEvents();

        return app;
    }
    

    public static IMessageSubscriber SubscribeApplication(this IMessageSubscriber subscriber)
    {
        subscriber.SubscribeToExternalProjectEvents();
        subscriber.SubscribeToExternalProjectGroupEvents();
        subscriber.SubscribeToExternalPermissionSchemeEvents();
        subscriber.SubscribeToExternalIssueEvents();
        subscriber.SubscribeToExternalSprintEvents();

        return subscriber;
    }

    private static IMessageSubscriber SubscribeToExternalProjectEvents(this IMessageSubscriber subscriber)
    {
        subscriber.Event<ProjectCreated>();
        subscriber.Event<ProjectJoined>();
        subscriber.Event<ProjectLeft>();
        subscriber.Event<ProjectUpdated>();
        subscriber.Event<UserInvitedToProject>();

        return subscriber;
    }

    private static IMessageSubscriber SubscribeToExternalProjectGroupEvents(this IMessageSubscriber subscriber)
    {
        subscriber.Event<ProjectGroupCreated>();
        subscriber.Event<ProjectGroupUpdated>();
        subscriber.Event<ProjectGroupDeleted>();

        return subscriber;
    }

    private static IMessageSubscriber SubscribeToExternalPermissionSchemeEvents(this IMessageSubscriber subscriber)
    {
        subscriber.Event<CustomPermissionSchemeCreated>();
        subscriber.Event<ProjectPermissionSchemeUpdated>();
        subscriber.Event<ProjectPermissionSchemeDeleted>();

        return subscriber;
    }

    private static IMessageSubscriber SubscribeToExternalIssueEvents(this IMessageSubscriber subscriber)
    {
        subscriber.Event<IssueCreated>();
        subscriber.Event<IssueUpdated>();
        subscriber.Event<IssueDeleted>();

        return subscriber;
    }

    private static IMessageSubscriber SubscribeToExternalSprintEvents(this IMessageSubscriber subscriber)
    {
        subscriber.Event<AddedIssueToSprint>();
        subscriber.Event<EndedSprint>();
        subscriber.Event<RemovedIssueFromSprint>();
        subscriber.Event<SprintCreated>();
        subscriber.Event<SprintDeleted>();
        subscriber.Event<SprintUpdated>();
        subscriber.Event<StartedSprint>();

        return subscriber;
    }
}