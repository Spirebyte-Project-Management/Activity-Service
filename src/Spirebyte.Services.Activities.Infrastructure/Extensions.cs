using System;
using System.Linq;
using Convey;
using Convey.Auth;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Convey.Discovery.Consul;
using Convey.Docs.Swagger;
using Convey.HTTP;
using Convey.LoadBalancing.Fabio;
using Convey.MessageBrokers;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.Outbox;
using Convey.MessageBrokers.Outbox.Mongo;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Persistence.MongoDB;
using Convey.Persistence.Redis;
using Convey.Security;
using Convey.Tracing.Jaeger;
using Convey.Tracing.Jaeger.RabbitMQ;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Convey.WebApi.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Open.Serialization.Json;
using Spirebyte.Services.Activities.Application;
using Spirebyte.Services.Activities.Application.Activities.Services.Interfaces;
using Spirebyte.Services.Activities.Application.Projects.Events.External;
using Spirebyte.Services.Activities.Application.Services.Interfaces;
using Spirebyte.Services.Activities.Core.Repositories;
using Spirebyte.Services.Activities.Infrastructure.Configuration;
using Spirebyte.Services.Activities.Infrastructure.Contexts;
using Spirebyte.Services.Activities.Infrastructure.Decorators;
using Spirebyte.Services.Activities.Infrastructure.Exceptions;
using Spirebyte.Services.Activities.Infrastructure.Mongo.Documents;
using Spirebyte.Services.Activities.Infrastructure.Mongo.Repositories;
using Spirebyte.Services.Activities.Infrastructure.Services;

namespace Spirebyte.Services.Activities.Infrastructure;

public static class Extensions
{
    public static string ToUserGroup(this Guid userId)
    {
        return userId.ToString("N").ToUserGroup();
    }

    public static string ToUserGroup(this string userId)
    {
        return $"users:{userId}";
    }

    public static string ToProjectGroup(this string projectId)
    {
        return $"project:{projectId}";
    }

    public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
    {
        builder.Services.AddTransient<IMessageBroker, MessageBroker>();

        builder.Services.AddTransient<IAppContextFactory, AppContextFactory>();
        builder.Services.AddTransient(ctx => ctx.GetRequiredService<IAppContextFactory>().Create());

        builder.Services.AddSingleton<IActivityRepository, ActivityRepository>();

        builder.Services.TryDecorate(typeof(ICommandHandler<>), typeof(OutboxCommandHandlerDecorator<>));
        builder.Services.TryDecorate(typeof(IEventHandler<>), typeof(OutboxEventHandlerDecorator<>));

        builder.Services
            .AddTransient<IHubService, HubService>()
            .AddTransient<IHubWrapper, HubWrapper>();
        builder.Services.AddGrpc();

        return builder
            .AddErrorHandler<ExceptionToResponseMapper>()
            .AddJwt()
            .AddQueryHandlers()
            .AddInMemoryQueryDispatcher()
            .AddInMemoryDispatcher()
            .AddHttpClient()
            .AddConsul()
            .AddFabio()
            .AddRabbitMq(plugins: p => p.AddJaegerRabbitMqPlugin())
            .AddMessageOutbox(o => o.AddMongo())
            .AddMongo()
            .AddRedis()
            .AddJaeger()
            .AddRedis()
            .AddMongoRepository<ActivityDocument, Guid>("activities")
            .AddSignalR()
            .AddWebApiSwaggerDocs()
            .AddSecurity();
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseErrorHandler()
            .UseSwaggerDocs()
            .UseJaeger()
            .UseConvey()
            .UseAccessTokenValidator()
            .UsePublicContracts<ContractAttribute>()
            .UseAuthentication()
            .UseStaticFiles()
            .UseRabbitMq()
            .SubscribeToExternalProjectEvents();

        return app;
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

    internal static CorrelationContext GetCorrelationContext(this IHttpContextAccessor accessor)
    {
        if (accessor.HttpContext is null) return null;

        if (!accessor.HttpContext.Request.Headers.TryGetValue("x-correlation-context", out var json)) return null;

        var jsonSerializer = accessor.HttpContext.RequestServices.GetRequiredService<IJsonSerializer>();
        var value = json.FirstOrDefault();

        return string.IsNullOrWhiteSpace(value) ? null : jsonSerializer.Deserialize<CorrelationContext>(value);
    }

    public static string GetUserIpAddress(this HttpContext context)
    {
        if (context is null) return string.Empty;

        var ipAddress = context.Connection.RemoteIpAddress?.ToString();
        if (context.Request.Headers.TryGetValue("x-forwarded-for", out var forwardedFor))
        {
            var ipAddresses = forwardedFor.ToString().Split(",", StringSplitOptions.RemoveEmptyEntries);
            if (ipAddresses.Any()) ipAddress = ipAddresses[0];
        }

        return ipAddress ?? string.Empty;
    }

    private static IConveyBuilder AddSignalR(this IConveyBuilder builder)
    {
        var options = builder.GetOptions<SignalrOptions>("signalR");
        builder.Services.AddSingleton(options);
        var signalR = builder.Services.AddSignalR();
        if (!options.Backplane.Equals("redis", StringComparison.InvariantCultureIgnoreCase)) return builder;

        var redisOptions = builder.GetOptions<RedisOptions>("redis");
        signalR.AddRedis(redisOptions.ConnectionString);

        return builder;
    }
}