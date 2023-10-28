module TodoApi.Todos

open System
open Giraffe
open Giraffe.EndpointRouting
open Microsoft.AspNetCore.Http

module Handlers =
    let viewTodos =
        fun (next:HttpFunc) (ctx:HttpContext) ->
            ctx.WriteTextAsync("Viewing all todos")
        

let apiTodoRoutes =
    [
        GET [
            //routef "/%O" Handlers.viewTodo
            route "" Handlers.viewTodos
        ]
        // POST [
        //     route "" Handlers.createTodo
        // ]
        // PUT [
        //     routef "/%O" Handlers.updateTodo
        // ]
        // DELETE [
        //     routef "/%O" Handlers.deleteTodo
        // ]
    ]