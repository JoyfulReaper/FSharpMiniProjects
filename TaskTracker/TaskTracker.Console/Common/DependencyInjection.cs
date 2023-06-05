using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TaskTracker.ApiClient;
using TaskTracker.Console.Features.Menu;
using TaskTracker.Console.Features.Tasks;

namespace TaskTracker.Console.Common;
internal static class DependencyInjection
{
    internal static IHost SetupDi(string[] args)
    {
        IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<TaskApplicationHostedService>();
                services.AddTaskClient(hostContext.Configuration);
                services.AddTransient<MenuService>();
                services.AddTransient<TaskService>();
            })
            .ConfigureLogging(logging => {
                logging.SetMinimumLevel(LogLevel.None);
            })
            .Build();

        return host;
    }
}
