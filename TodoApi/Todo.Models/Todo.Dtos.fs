namespace Todo.Models.Dtos

open System

[<CLIMutable>]
type Todo = {
    Id: int
    Title: string
    Description: string
    Completed: bool
}

[<CLIMutable>]
type TodoRequest = {
    Title: string
    Description: string
    Completed: Nullable<bool>
}