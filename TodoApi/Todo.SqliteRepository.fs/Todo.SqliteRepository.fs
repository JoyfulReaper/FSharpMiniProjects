namespace Todo.SqliteRepository

open Todo.Abstractions
open Todo.Models
open Todo.SqlRepository.Entities
open Todo.SqliteRepository.Mapping
open Microsoft.EntityFrameworkCore
open FsToolkit.ErrorHandling

module SqlTodoRepository =
    
    let todoWithNameExists (title : string) =
        async {
            use context = new TodoContext()
            let! todo = context.Todos.SingleOrDefaultAsync(fun t -> t.Title = title) |> Async.AwaitTask
            return todo <> null
        }
    
    let addTodo (request : TodoRequest) =
        asyncResult {
            let title = request.Title |> Title.value
            let! exists = todoWithNameExists title 
            
            match exists with
            | true ->
                return! Error <| TodoAlreadyExists title
            | false ->
                use context = new TodoContext()
                let entity = TodoEntity.ofToDoRequest request
                let tracked = context.Todos.Add(entity)
                let! rows = context.SaveChangesAsync() |> Async.AwaitTask
                return! Ok entity
        }
        
    let removeTodo (todoId : TodoId) =
        asyncResult {
            use context = new TodoContext()
            let todoId = todoId |> TodoId.value
            let! todo = context.Todos.SingleOrDefaultAsync(fun t -> t.Id = todoId) |> Async.AwaitTask
            match todo with 
            | null ->
                return! Error <| TodoDoesNotExist (string todoId)
            | todo -> 
                let tracked = context.Todos.Remove(todo)
                let! rows = context.SaveChangesAsync() |> Async.AwaitTask
                return! Ok ()
        }
        
    let updateTodo (todo: Todo.Models.Todo) =
        asyncResult {
            use context = new TodoContext()
            let todoId = todo.Id |> TodoId.value
            let! existing = context.Todos.SingleOrDefaultAsync(fun t -> t.Id = todoId) |> Async.AwaitTask
            match existing with
            | null ->
                return! Error <| TodoDoesNotExist (string todoId)
            | existing ->
                let existingDto = TodoEntity.toDto existing
                let updatedDto = todo |> Todo.toDto
                return! Ok { existingDto with Title = updatedDto.Title; Description = updatedDto.Description; Completed = updatedDto.Completed }
        }
        
    let getTodos () =
        async {
            use context = new TodoContext()
            let! todos = context.Todos.ToListAsync() |> Async.AwaitTask
            return todos |> List.ofSeq |> List.map TodoEntity.toDto
        }
        
    let getTodo (id : TodoId) =
        async {
            use context = new TodoContext()
            let todoId = id |> TodoId.value
            let! todo = context.Todos.SingleOrDefaultAsync(fun t -> t.Id = todoId) |> Async.AwaitTask
            match todo with
            | null ->
                return None
            | todo ->
                return Some (TodoEntity.toModel todo)
        }