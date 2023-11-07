namespace MiningGame.Models

open FsToolkit.ErrorHandling
open MiningGame.Models.Errors

type Material =
    | Dirt
    | Stone
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

type ItemIds =
    | Dirt = 1
    | Stone = 2
    | CopperOre = 3
    | IronOre = 4
    | GoldOre = 5
    | DiamondOre = 6

module Item =
    let createStone quantity =
        {
            Id = ItemIds.Stone |> int
            Name = "Stone"
            Description = sprintf "A piece of stone."
            CraftRecipe = None
            Material = Some Stone
            Inventory = None
            Quantity = quantity
        }
        
    let createOre (material:Material) (quantity:int) =
        result {
            return!
                match material with
                | Copper ->
                    Ok {
                        Id = ItemIds.CopperOre |> int
                        Name = "Copper Ore"
                        Description = sprintf "A piece of copper ore."
                        CraftRecipe = None
                        Material = Some Copper
                        Inventory = None
                        Quantity = quantity
                    }
                | Iron ->
                   Ok {
                        Id = ItemIds.IronOre |> int
                        Name = "Iron Ore"
                        Description = sprintf "A piece of iron ore."
                        CraftRecipe = None
                        Material = Some Iron
                        Inventory = None
                        Quantity = quantity
                    }
                | Gold ->
                   Ok {
                        Id = ItemIds.GoldOre |> int
                        Name = "Gold Ore"
                        Description = sprintf "A piece of gold ore."
                        CraftRecipe = None
                        Material = Some Gold
                        Inventory = None
                        Quantity = quantity
                    }
                | Diamond ->
                    Ok {
                        Id = ItemIds.DiamondOre |> int
                        Name = "Diamond Ore"
                        Description = sprintf "A piece of diamond ore."
                        CraftRecipe = None
                        Material = Some Diamond
                        Inventory = None
                        Quantity = quantity
                    }
                | _ ->
                    Error <| InvalidMaterial (string material)
        }