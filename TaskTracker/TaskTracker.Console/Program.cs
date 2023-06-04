using TaskTracker.Console.Common;

var host = DependencyInjection.SetupDi(args);
await host.StartAsync();

host.Dispose();