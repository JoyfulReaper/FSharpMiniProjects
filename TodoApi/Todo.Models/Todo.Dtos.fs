namespace Todo.Models.Dtos

[<CLIMutable>]
type Todo = {
    Id: int
    Title: string
    Description: string
    Completed: bool
}

