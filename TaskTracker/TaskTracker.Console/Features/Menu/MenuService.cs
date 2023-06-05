using System;
using TaskTracker.Console.Common.Enums;
using TaskTracker.Console.Features.Tasks;

namespace TaskTracker.Console.Features.Menu;

internal class MenuService
{
    private readonly TaskService _taskService;

    public MenuService(TaskService taskService)
    {
        _taskService = taskService;
    }

    internal void DisplayHeader()
    {
        System.Console.WriteLine("Task Tracker 1.0.0");
    }

    internal void DisplayMenu()
    {
        System.Console.WriteLine();
        System.Console.WriteLine("1. Create task");
        System.Console.WriteLine("2. View tasks");
        System.Console.WriteLine("3. Exit");
    }

    internal MenuChoice GetOption()
    {
        System.Console.Write("Enter option: ");
        var option = System.Console.ReadLine();

        if (!int.TryParse(option, out int parseResult))
        {
            return MenuChoice.Invalid;
        }

        var result = (MenuChoice)parseResult;
        if (!Enum.IsDefined(typeof(MenuChoice), result))
        {
            return MenuChoice.Invalid;
        }

        return result;
    }

    internal async Task ProcessOptionAsync(MenuChoice option)
    {
        switch (option)
        {
            case MenuChoice.CreateTask:
                await _taskService.CreateTaskAsync();
                break;
            case MenuChoice.ViewTasks:
                await _taskService.ListTasksAsync();
                break;
            case MenuChoice.Exit:
                Environment.Exit(Environment.ExitCode);
                break;
            case MenuChoice.Invalid:
                System.Console.WriteLine("Invalid option");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(option), option, null);
        }
    }   
}
