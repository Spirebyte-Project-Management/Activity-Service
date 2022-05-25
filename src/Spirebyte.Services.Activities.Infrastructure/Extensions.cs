using System;
using Convey;
using Convey.Auth;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Convey.Docs.Swagger;
using Convey.HTTP;
using Convey.LoadBalancing.Fabio;
using Convey.MessageBrokers.Outbox;
using Convey.MessageBrokers.Outbox.Mongo;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Metrics.AppMetrics;
using Convey.Persistence.MongoDB;
using Convey.Persistence.Redis;
using Convey.Security;
using Convey.Tracing.Jaeger;
using Convey.Tracing.Jaeger.RabbitMQ;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Convey.WebApi.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using Spirebyte.Services.Activities.Application;
using Spirebyte.Services.Activities.Application.Activities.Services.Interfaces;
using Spirebyte.Services.Activities.Application.Services.Interfaces;
using Spirebyte.Services.Activities.Core.Repositories;
using Spirebyte.Services.Activities.Infrastructure.Configuration;
using Spirebyte.Services.Activities.Infrastructure.Decorators;
using Spirebyte.Services.Activities.Infrastructure.Exceptions;
using Spirebyte.Services.Activities.Infrastructure.Mongo.Documents;
using Spirebyte.Services.Activities.Infrastructure.Mongo.Repositories;
using Spirebyte.Services.Activities.Infrastructure.Mongo.Serializers;
using Spirebyte.Services.Activities.Infrastructure.ServiceDiscovery;
using Spirebyte.Services.Activities.Infrastructure.Services;
using Spirebyte.Shared.Contexts;

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

        builder.Services.AddSingleton<IActivityRepository, ActivityRepository>();

        builder.Services.TryDecorate(typeof(ICommandHandler<>), typeof(OutboxCommandHandlerDecorator<>));
        builder.Services.TryDecorate(typeof(IEventHandler<>), typeof(OutboxEventHandlerDecorator<>));

        builder.Services.AddSharedContexts();

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
            .AddCustomConsul()
            .AddFabio()
            .AddRabbitMq(plugins: p => p.AddJaegerRabbitMqPlugin())
            .AddMessageOutbox(o => o.AddMongo())
            .AddMongo()
            .AddRedis()
            .AddMetrics()
            .AddJaeger()
            .AddRedis()
            .AddMongoRepository<ActivityDocument, Guid>("activities")
            .AddSignalR()
            .AddWebApiSwaggerDocs()
            .AddSecurity();
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        BsonSerializer.RegisterDiscriminatorConvention(typeof(object), NullDiscriminatorConvention.Instance);

        app.UseErrorHandler()
            .UseSwaggerDocs()
            .UseJaeger()
            .UseConvey()
            .UseMetrics()
            .UseAccessTokenValidator()
            .UsePublicContracts<ContractAttribute>()
            .UseAuthentication()
            .UseStaticFiles()
            .UseRabbitMq()
            .SubscribeApplication();

        return app;
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