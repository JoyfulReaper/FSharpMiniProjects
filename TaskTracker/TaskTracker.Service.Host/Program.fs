open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe
open TaskTracker.Repository
open TaskTracker.Repository.Sql.TaskRepository

let webApp =
    choose [
        route "/ping"   >=> text "pong"
        route "/"       >=> TaskService.getTaskHandler ]

let configureApp (app : IApplicationBuilder) =
    // Add Giraffe to the ASP.NET Core pipeline
    app.UseGiraffe webApp

let configureServices (services : IServiceCollection) =
    // Add Giraffe dependencies
    services.AddGiraffe() |> ignore
    services.AddTransient<ITaskRepository, SqlTaskRepository>() |> ignore

[<EntryPoint>]
let main _ =
    Dapper.FSharp.SQLite.OptionTypes.register()


    let builder = Host.CreateDefaultBuilder()
                    .ConfigureWebHostDefaults(
                        fun webHostBuilder ->
                            webHostBuilder
                                .Configure(configureApp)
                                .ConfigureServices(configureServices)
                                |> ignore)
                    .Build()
    builder.Services.GetRequiredService<ITaskRepository>().EnsureTableCreated() |> Async.RunSynchronously
    builder.Run()
    0