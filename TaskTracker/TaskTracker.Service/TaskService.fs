module TaskService

open Giraffe
open Microsoft.AspNetCore.Http
open TaskTracker.Repository
open TaskTracker.Models
open System

let getAllTaskHandler : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let taskRepository = ctx.GetService<ITaskRepository>()
            let! tasks = taskRepository.GetAll()
            return! json (tasks |> List.map Task.toDto) next ctx
        }

let getTaskHandler taskId : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let taskRepository = ctx.GetService<ITaskRepository>()
            let! result = taskRepository.Get taskId

            match result with
            | Some task -> return! json (Task.toDto task) next ctx
            | None -> return! RequestErrors.NOT_FOUND {Message = "Task not found"} next ctx
        }

let createTaskHandler : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let taskRepository = ctx.GetService<ITaskRepository>()
            let! task = ctx.BindJsonAsync<TaskRequest>()
            let! createdTask = taskRepository.Create <| Task.fromTaskRequest task
            
            match createdTask with
            | None ->  return! RequestErrors.BAD_REQUEST {Message = "Task could not be created"} next ctx
            | Some x -> 
                ctx.SetHttpHeader("Location", sprintf "/task/%s" (x.TaskId.ToString()))
                return! Successful.CREATED (Task.toDto x) next ctx
        }

let updateTaskHandler (taskId: System.Guid) : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let taskRepository = ctx.GetService<ITaskRepository>()
            let! taskUpdateRequest = ctx.BindJsonAsync<TaskUpdateRequest>()
            let taskFromUpdateRequest = Task.fromTaskUpdateRequest taskUpdateRequest
            
            let! existingTask = taskRepository.Get taskId

            match existingTask with
            | Some existingTask -> 
                let taskToUpdate = { taskFromUpdateRequest with TaskId = existingTask.TaskId }
                let! updatedTask = taskRepository.Update taskToUpdate
                return! json (Task.toDto (Option.get updatedTask)) next ctx
            | None -> return! RequestErrors.NOT_FOUND {Message = "Task not found"} next ctx
        }

let deleteTaskHandler (taskId: System.Guid) : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let taskRepository = ctx.GetService<ITaskRepository>()
            let! existingTask = taskRepository.Get taskId

            match existingTask with
            | Some existingTask -> 
                let! deletedTask = taskRepository.Delete existingTask.TaskId
                return! Successful.NO_CONTENT next ctx
            | None -> return! RequestErrors.NOT_FOUND {Message = "Task not found"} next ctx
        }

let completeTaskHandler (taskId: System.Guid) : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let taskRepository = ctx.GetService<ITaskRepository>()
            let! existingTask = taskRepository.Get taskId

            match existingTask with
            | Some existingTask -> 
                let taskToUpdate = { existingTask with Completed = true; DateCompleted = Some(DateTime.Now) }
                let! updatedTask = taskRepository.Update taskToUpdate
                return! json (Task.toDto (Option.get updatedTask)) next ctx
            | None -> return! RequestErrors.NOT_FOUND {Message = "Task not found"} next ctx
        }