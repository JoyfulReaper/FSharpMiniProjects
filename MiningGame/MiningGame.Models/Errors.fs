namespace MiningGame.Models.Errors

type ModelError =
    | InvalidMaterial of string
    | InvalidQuantity of int
    member this.Message =
        match this with
        | InvalidMaterial material -> sprintf "Invalid material: %s" material
        | InvalidQuantity quantity -> sprintf "Invalid quantity: %i" quantity

