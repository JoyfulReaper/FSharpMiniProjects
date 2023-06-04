using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TaskTracker.ApiClient.HttpClients;
using TaskTracker.ApiClient.Options;

namespace TaskTracker.ApiClient;

public static class DependencyInjection
{
    public static IServiceCollection AddTaskClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TaskApiClientOptions>(
            configuration.GetSection(nameof(TaskApiClientOptions)));

        services.AddTransient<ITaskClient, TaskClient>();

        return services;
    }
}
