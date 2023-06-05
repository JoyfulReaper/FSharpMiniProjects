using Microsoft.Extensions.Hosting;
using TaskTracker.ApiClient.HttpClients;
using TaskTracker.Console.Features.Menu;

namespace TaskTracker.Console.Features.Tasks;
internal class TaskApplicationHostedService : IHostedService
{
    private readonly ITaskClient _taskClient;
    private readonly MenuService _menuService;

    public TaskApplicationHostedService(ITaskClient taskClient, MenuService menuService)
    {
        _taskClient = taskClient;
        _menuService = menuService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        MenuService.DisplayHeader();
        while (true)
        {
            await _menuService.DisplayMenuAsync();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
