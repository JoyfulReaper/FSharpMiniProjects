namespace Todo.SqliteRepository.Mapping

open Todo.Models
open Todo.SqlRepository.Entities
open FsToolkit.ErrorHandling.ResultCE

module TodoEntity =
    
    let ofTodo (model: Todo.Models.Todo) =
        let title = model.Title |> Title.value
        let description = model.Description |> Description.value
        Todo(Title = title, Description = description, Completed = model.Completed)
        
    let toModel (entity: Todo) =
        result {
            let! id = entity.Id |> TodoId.create
            let! title = entity.Title |> Title.create
            let! description = entity.Description |> Description.create
            return! Ok { Todo.Id = id; Todo.Title = title; Todo.Description = description; Todo.Completed = entity.Completed }
        }
        
    let toDto (entity: Todo) : Dtos.Todo =
        { Id = entity.Id; Title = entity.Title; Description = entity.Description; Completed = entity.Completed }
        
    let ofToDoRequest (todoRequest: TodoRequest) =
        let title = todoRequest.Title |> Title.value
        let description = todoRequest.Description |> Description.value
        Todo(Title = title, Description = description, Completed = defaultArg todoRequest.Completed false)