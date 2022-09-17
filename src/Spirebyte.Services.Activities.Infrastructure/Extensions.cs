using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using Spirebyte.Framework.DAL.MongoDb;
using Spirebyte.Services.Activities.Application.Activities.Services.Interfaces;
using Spirebyte.Services.Activities.Core.Repositories;
using Spirebyte.Services.Activities.Infrastructure.Configuration;
using Spirebyte.Services.Activities.Infrastructure.Mongo.Documents;
using Spirebyte.Services.Activities.Infrastructure.Mongo.Repositories;
using Spirebyte.Services.Activities.Infrastructure.Mongo.Serializers;
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

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSignalR();
        
        services
            .AddTransient<IHubService, HubService>()
            .AddTransient<IHubWrapper, HubWrapper>();
        services.AddGrpc();

        services.AddMongo(configuration)
            .AddMongoRepository<ActivityDocument, Guid>("activities");

        services.AddTransient<IActivityRepository, ActivityRepository>();

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder)
    {
        BsonSerializer.RegisterDiscriminatorConvention(typeof(object), NullDiscriminatorConvention.Instance);
        
        return builder;
    }
}