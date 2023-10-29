namespace Todo.Repository

open Todo.Models
open FsToolkit.ErrorHandling.ResultCE

type RepositoryError =
    | TodoAlreadyExists of string
    | TodoDoesNotExist of string
    | ValidationError of ValidationError
    member this.Message =
        match this with
        | ValidationError e -> e.Message
        | TodoAlreadyExists msg -> msg |> sprintf "Todo Already Exists: %s"
        | TodoDoesNotExist msg -> msg |> sprintf "Todo Does Not Exist: %s"

module Result =
    let ofOption error (option : 'a option) =
        match option with
        | Some value -> Ok value
        | None -> error |> Error

module TodoRepository =

    let mutable private todos : Todo list = []

    let getNextId () =
        result {
            if List.length todos = 0 then
                let! id = 1 |> TodoId.create |> Result.mapError ValidationError
                return id
            else
                let! nextId = todos |> List.maxBy (fun t -> t.Id |> TodoId.value) |> fun t -> (t.Id |> TodoId.value) + 1 |> TodoId.create |> Result.mapError ValidationError
                return! Ok nextId
        }
        

    let addTodo (todo : Todo) =
        result {
            if List.exists (fun t -> t.Title = todo.Title) todos then
                return! TodoAlreadyExists (todo.Title |> Title.value) |> Error
            else
                let! nextId = getNextId ()
                let todo = { todo with Id = nextId }
                todos <- todo :: todos
                return! Ok ()
        }
        

    let removeTodo (todoId : TodoId) =
        if List.exists (fun t -> t.Id = todoId) todos then
            todos <- todos |> List.filter (fun t -> t.Id <> todoId)
            Ok ()
        else
            TodoDoesNotExist (todoId |> TodoId.value |> string) |> Error

    let updateTodo (todo : Todo) =
        if List.exists (fun t -> t.Id = todo.Id) todos then
            todos <- todos |> List.map (fun t -> if t.Id = todo.Id then todo else t)
            Ok ()
        else
            TodoDoesNotExist (todo.Id |> TodoId.value |> string) |> Error
    
    let getTodos () =
        todos

    let getTodo (id : TodoId) =
        let todo = 
            todos |> List.tryFind (fun t -> t.Id = id)
        todo