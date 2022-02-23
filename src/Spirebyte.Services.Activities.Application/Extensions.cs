using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Convey.MessageBrokers.CQRS;
using Spirebyte.Services.Activities.Application.Issues.Events.External;
using Spirebyte.Services.Activities.Application.PermissionSchemes.Events.External;
using Spirebyte.Services.Activities.Application.ProjectGroups.Events.External;
using Spirebyte.Services.Activities.Application.Projects.Events.External;
using Spirebyte.Services.Activities.Application.Sprints.Events.External;

namespace Spirebyte.Services.Activities.Application;

public static class Extensions
{
    public static IConveyBuilder AddApplication(this IConveyBuilder builder)
    {
        return builder
            .AddCommandHandlers()
            .AddEventHandlers()
            .AddInMemoryCommandDispatcher()
            .AddInMemoryEventDispatcher();
    }

    public static IBusSubscriber SubscribeApplication(this IBusSubscriber subscriber)
    {
        subscriber.SubscribeToExternalProjectEvents();
        subscriber.SubscribeToExternalProjectGroupEvents();
        subscriber.SubscribeToExternalPermissionSchemeEvents();
        subscriber.SubscribeToExternalIssueEvents();
        subscriber.SubscribeToExternalSprintEvents();

        return subscriber;
    }
    
    private static IBusSubscriber SubscribeToExternalProjectEvents(this IBusSubscriber subscriber)
    {
        subscriber.SubscribeEvent<ProjectCreated>();
        subscriber.SubscribeEvent<ProjectJoined>();
        subscriber.SubscribeEvent<ProjectLeft>();
        subscriber.SubscribeEvent<ProjectUpdated>();
        //subscriber.SubscribeEvent<UserInvitedToProject>();

        return subscriber;
    }
    
    private static IBusSubscriber SubscribeToExternalProjectGroupEvents(this IBusSubscriber subscriber)
    {
        subscriber.SubscribeEvent<ProjectGroupCreated>();
        subscriber.SubscribeEvent<ProjectGroupUpdated>();
        subscriber.SubscribeEvent<ProjectGroupDeleted>();

        return subscriber;
    }
    
    private static IBusSubscriber SubscribeToExternalPermissionSchemeEvents(this IBusSubscriber subscriber)
    {
        subscriber.SubscribeEvent<CustomPermissionSchemeCreated>();
        subscriber.SubscribeEvent<ProjectPermissionSchemeUpdated>();
        subscriber.SubscribeEvent<ProjectPermissionSchemeDeleted>();

        return subscriber;
    }
    
    private static IBusSubscriber SubscribeToExternalIssueEvents(this IBusSubscriber subscriber)
    {
        subscriber.SubscribeEvent<IssueCreated>();
        subscriber.SubscribeEvent<IssueUpdated>();
        subscriber.SubscribeEvent<IssueDeleted>();

        return subscriber;
    }
    
    private static IBusSubscriber SubscribeToExternalSprintEvents(this IBusSubscriber subscriber)
    {
        subscriber.SubscribeEvent<AddedIssueToSprint>();
        subscriber.SubscribeEvent<EndedSprint>();
        subscriber.SubscribeEvent<RemovedIssueFromSprint>();
        subscriber.SubscribeEvent<SprintCreated>();
        subscriber.SubscribeEvent<SprintDeleted>();
        subscriber.SubscribeEvent<SprintUpdated>();
        subscriber.SubscribeEvent<StartedSprint>();

        return subscriber;
    }
}