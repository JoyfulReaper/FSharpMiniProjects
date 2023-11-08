namespace MiningGame.Mining

open System
open MiningGame.Models
open FsToolkit.ErrorHandling
open MiningGame.Models.Errors

type MiningError =
    | ModelError of ModelError
    | SmeltingError of string
    member this.Message =
        match this with
        | ModelError e -> e.Message
        | SmeltingError e -> e

module Mining =
    
    let smelt (item: Item) =
        result {
            let! one = PositiveQuantity.create 1 |> Result.mapError ModelError
            let! ingot =
                match item.Material with
                | Some Copper -> Ok <| Item.createIngot Copper one
                | Some Iron -> Ok <| Item.createIngot Iron one
                | Some Gold -> Ok <| Item.createIngot Gold one
                | _ -> Error <| SmeltingError "Cannot smelt this ore"
            return! ingot |> Result.mapError ModelError
        }
    
    let goMining () =
        result {
            // 5 percent chance for diamond, 10 percent chance for gold, 15 percent chance for iron, 30 percent chance for copper, 30 percent chance for stone
            let randNum = Random.Shared.Next(1, 101)
            let! one = PositiveQuantity.create 1 |> Result.mapError ModelError
            
            let! ore =
                (if randNum <= 5 then
                    Ok <| Item.createDiamond one
                elif randNum <= 15 then
                    Item.createOre Gold one
                elif randNum <= 30 then
                    Item.createOre Iron one
                elif randNum <= 60 then
                    Item.createOre Copper one
                else
                    Ok <| Item.createStone one) |> Result.mapError ModelError
            return ore   
        }
        
        