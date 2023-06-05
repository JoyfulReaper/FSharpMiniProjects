using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.ApiClient.Contracts;
using TaskTracker.ApiClient.HttpClients;

namespace TaskTracker.Console.Features.Tasks;

internal class TaskService
{
    private readonly ITaskClient _taskClient;

    public TaskService(ITaskClient taskClient)
    {
        _taskClient = taskClient;
    }

    public async Task ListTasksAsync()
    {
        var allTasks = await _taskClient.GetAllAsync();
        foreach (var task in allTasks)
        {
            System.Console.WriteLine($"Task: {task.Title}");
        }
    }

    public async Task CreateTaskAsync()
    {
        System.Console.Write("Enter task title: ");
        var title = System.Console.ReadLine();
        System.Console.Write("Enter task description: ");
        var description = System.Console.ReadLine();

        var task = new TaskRequest {
            Title = title,
            Description = description
        };

        await _taskClient.CreateAsync(task);
    }
}
