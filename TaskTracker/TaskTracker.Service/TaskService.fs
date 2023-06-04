module TaskService

open Giraffe
open Microsoft.AspNetCore.Http
open TaskTracker.Repository
open TaskTracker.Models
open Giraffe.HttpStatusCodeHandlers.RequestErrors


let getTaskHandler taskId : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let taskRepository = ctx.GetService<ITaskRepository>()
            let! result = taskRepository.Get taskId

            match result with
            | Some task -> return! json (Task.toDto task) next ctx
            | None -> return! (setStatusCode 404 >=> json {Message = "Task not found"}) next ctx
        }

let createTaskHandler : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let taskRepository = ctx.GetService<ITaskRepository>()
            let! task = ctx.BindJsonAsync<TaskRequest>()
            let! createdTask = taskRepository.Create (Task.fromTaskRequest task)
            return! json createdTask next ctx
        }