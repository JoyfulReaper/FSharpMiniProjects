module TodoApi.Todos

open System
open Giraffe
open Giraffe.EndpointRouting
open Microsoft.AspNetCore.Http
open Todo.Models
open Todo.ListRepository
open FsToolkit.ErrorHandling

let getMessage (errors:ValidationError list) =
    let errors = errors |> List.map (fun e -> e.Message)
    String.Join (Environment.NewLine, errors)

module Handlers =
    let viewTodos =
        fun (next:HttpFunc) (ctx:HttpContext) ->
            TodoRepository.getTodos()
            |> List.map Todo.toDto
            |> ctx.WriteJsonAsync
            
    let viewTodo (id:int) =
        fun (next:HttpFunc) (ctx:HttpContext) ->
            task {
                let id = TodoId.create id 
                match id with
                | Error e ->
                    return! ServerErrors.INTERNAL_ERROR "Failed to create id" next ctx
                | Ok id ->
                    match TodoRepository.getTodo id with
                    | None ->
                        return! RequestErrors.NOT_FOUND "Todo not found" next ctx
                    | Some todo ->
                        return! Successful.OK (todo |> Todo.toDto) next ctx
            }
            
    let createTodo =
        fun (next:HttpFunc) (ctx:HttpContext) ->
            task {
                let! request = ctx.BindJsonAsync<Dtos.TodoRequest>()
                let model = TodoRequest.ofDto request
                match model with
                | Error e ->
                    return! RequestErrors.BAD_REQUEST (getMessage e) next ctx
                | Ok model ->
                    match TodoRepository.addTodo model with
                    | Error e ->
                        return! RequestErrors.BAD_REQUEST (e.Message) next ctx
                    | Ok todo ->
                        return! Successful.CREATED (todo |> Todo.toDto) next ctx
            }
    
    let deleteTodo (id:int) =
        fun (next:HttpFunc) (ctx:HttpContext) ->
            task {
                let todoId = TodoId.create id
                match todoId with
                | Error e ->
                    return! ServerErrors.INTERNAL_ERROR "Failed to create id" next ctx
                | Ok todoId ->
                    match TodoRepository.removeTodo todoId with
                    | Error e ->
                        return! RequestErrors.NOT_FOUND "Todo not found" next ctx
                    | Ok () ->
                        return! Successful.NO_CONTENT next ctx
            }

    let updateTodo (id:int) =
        fun (next:HttpFunc) (ctc:HttpContext) ->
            task {
                let! todo = ctc.BindJsonAsync<Dtos.Todo>()
                let model = Todo.ofDto todo
                match model with
                | Error e ->
                    return! ServerErrors.INTERNAL_ERROR "Failed to create id" next ctc
                | Ok model ->
                    match TodoRepository.updateTodo model with
                    | Error e ->
                        return! RequestErrors.NOT_FOUND "Todo not found" next ctc
                    | Ok () ->
                        return! Successful.NO_CONTENT next ctc
            }
let apiTodoRoutes =
    [
        GET [
            routef "/%i" Handlers.viewTodo
            route "" Handlers.viewTodos
        ]
        POST [
            route "" Handlers.createTodo
        ]
        PUT [
            routef "/%i" Handlers.updateTodo
        ]
        DELETE [
            routef "/%i" Handlers.deleteTodo
        ]
    ]