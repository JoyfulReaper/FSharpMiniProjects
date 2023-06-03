module TaskService

open Giraffe
open System
open Microsoft.AspNetCore.Http
open TaskTracker.Repository
open TaskTracker.Models

let getTaskHandler taskId : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let taskRepository = ctx.GetService<ITaskRepository>()
            //let taskId = ctx.BindQueryString<Guid>()
            let! task = taskRepository.Get taskId

            return! json task next ctx
        }

let createTaskHandler : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let taskRepository = ctx.GetService<ITaskRepository>()
            let! task = ctx.BindJsonAsync<TaskRequest>()
            let! createdTask = taskRepository.Create (Task.fromTaskRequest task)

            return! json createdTask next ctx
        }