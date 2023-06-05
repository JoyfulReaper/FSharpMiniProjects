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
        _menuService.DisplayHeader();
        while (true)
        {
            _menuService.DisplayMenu();
            var option = _menuService.GetOption();
            await _menuService.ProcessOptionAsync(option);
        }

        //var test = await _taskClient.GetAsync("06bd0b89-1b4e-4a58-a3f3-f964748703f2", cancellationToken);
        //var test2 = await _taskClient.GetAllAsync(cancellationToken);
        //throw new NotImplementedException();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
