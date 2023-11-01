namespace MiningGame.Models

open FsToolkit.ErrorHandling
open MiningGame.Models.Errors

type Material =
    | Dirt
    | Store
    | Copper
    | Iron
    | Gold
    | Diamond

type Inventory = Item list
and Item = {
    Id: int
    Name: string
    Description: string
    CraftRecipe: Item list option
    Material: Material option
    Inventory: Inventory option
    Quantity: int
}

type Player = {
    Inventory: Inventory
}

module Item =
    let createOre (material:Material) (quantity:int option) =
        result {
            return!
                match material with
                | Copper ->
                    Ok {
                        Id = 1
                        Name = "Copper Ore"
                        Description = sprintf "A piece of copper ore."
                        CraftRecipe = None
                        Material = Some Copper
                        Inventory = None
                        Quantity = defaultArg quantity 1
                    }
                | Iron ->
                   Ok {
                        Id = 2
                        Name = "Iron Ore"
                        Description = sprintf "A piece of iron ore."
                        CraftRecipe = None
                        Material = Some Iron
                        Inventory = None
                        Quantity = defaultArg quantity 1
                    }
                | Gold ->
                   Ok {
                        Id = 3
                        Name = "Gold Ore"
                        Description = sprintf "A piece of gold ore."
                        CraftRecipe = None
                        Material = Some Gold
                        Inventory = None
                        Quantity = defaultArg quantity 1
                    }
                | Diamond ->
                    Ok {
                        Id = 4
                        Name = "Diamond Ore"
                        Description = sprintf "A piece of diamond ore."
                        CraftRecipe = None
                        Material = Some Diamond
                        Inventory = None
                        Quantity = defaultArg quantity 1
                    }
                | _ ->
                    Error <| InvalidMaterial (string material)
        }