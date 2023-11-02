namespace Todo.Models

open FsToolkit.ErrorHandling.ValidationCE
open System

type ValidationError =
    | RequiredField of string
    | InvalidField of string
    member this.Name =
        match this with
        | RequiredField _ -> "Required Field is missing"
        | InvalidField _ -> "Field is invalid"
    member this.Message =
        match this with
        | RequiredField name -> sprintf "%s is required" name
        | InvalidField name -> sprintf "%s is invalid" name


[<AutoOpen>]
module Validation =
    let (|IsEmptyString|_|) (input : string) =
        if String.IsNullOrWhiteSpace input then
            Some input
        else
            None
    

type TodoId = private TodoId of int
module TodoId = 
    let create id = 
        if id < 1 then
            Error (InvalidField "Id")
        else
            Ok (TodoId id)

    let value (TodoId id) = id

type Title = private Title of string
module Title = 
    let create title = 
        match title with
        | IsEmptyString _ -> Error (RequiredField "Title")
        | _ -> 
            if title.Length > 50 then
                Error (InvalidField "Title")
            else
                Ok (Title title)

    let value (Title title) = title

type Description = private Description of string
module Description = 
    let create description = 
        match description with
        | IsEmptyString _ -> Error (RequiredField "Description")
        | _ -> 
            if description.Length > 500 then
                Error (InvalidField "Description")
            else
                Ok (Description description)

    let value (Description description) = description

type Todo =
    {
        Id: TodoId
        Title: Title
        Description: Description
        Completed: bool
    }
module Todo =
    let ofDto (dto: Dtos.Todo) : Result<Todo, ValidationError list>=
        validation {
            let! id = 
                dto.Id
                |> TodoId.create
                |> Result.mapError (fun ex -> [ex])
            and! title = 
                dto.Title
                |> Title.create
                |> Result.mapError (fun ex -> [ex])
            and! description = 
                dto.Description
                |> Description.create
                |> Result.mapError (fun ex -> [ex])
            
            return {
                Id = id
                Title = title
                Description = description
                Completed = dto.Completed
            }
        }
        
    let toDto (model: Todo) : Dtos.Todo =
        {
            Id = TodoId.value model.Id
            Title = Title.value model.Title
            Description = Description.value model.Description
            Completed = model.Completed
        }

    let toString (model: Todo) =
        sprintf "%i: %s - %s" (TodoId.value model.Id) (Title.value model.Title) (Description.value model.Description)
        
type TodoRequest =
    {
        Title: Title
        Description: Description
        Completed: bool option
    }
    
module TodoRequest =
    let ofDto (dto: Dtos.TodoRequest) : Result<TodoRequest, ValidationError list> =
        validation {
            let! title = 
                dto.Title
                |> Title.create
                |> Result.mapError (fun ex -> [ex])
            and! description = 
                dto.Description
                |> Description.create
                |> Result.mapError (fun ex -> [ex])
            
            return {
                Title = title
                Description = description
                Completed =  Option.ofNullable dto.Completed
            }
        }
        
    let toTodo (id: TodoId) (model: TodoRequest)  : Todo =
        {
            Id = id
            Title = model.Title
            Description = model.Description
            Completed = model.Completed |> Option.defaultValue false
        }