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
    Value: int
}

type Player = {
    Name: string
    Money: int
    Inventory: Inventory
}

type ItemIds =
    | Dirt = 1
    | Stone = 2
    | CopperOre = 3
    | IronOre = 4
    | GoldOre = 5
    | DiamondOre = 6
    | CopperIngot = 7
    | IronIngot = 8
    | GoldIngot = 9
    

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
            Value = 1
        }
        
    let createDiamond quantity =
        {
            Id = ItemIds.DiamondOre |> int
            Name = "Diamond Ore"
            Description = sprintf "A beautiful sparkling diamond."
            CraftRecipe = None
            Material = Some Diamond
            Inventory = None
            Quantity = quantity
            Value = 25
        }
        
    let createIngot (material:Material) (quantity:int) =
        result {
            return!
                match material with
                | Copper ->
                    Ok {
                        Id = ItemIds.CopperIngot |> int
                        Name = "Copper Ingot"
                        Description = sprintf "A piece of copper ingot."
                        CraftRecipe = None
                        Material = Some Copper
                        Inventory = None
                        Quantity = quantity
                        Value = 4
                    }
                | Iron ->
                    Ok {
                        Id = ItemIds.IronIngot |> int
                        Name = "Iron Ingot"
                        Description = sprintf "A piece of iron ingot."
                        CraftRecipe = None
                        Material = Some Iron
                        Inventory = None
                        Quantity = quantity
                        Value = 6
                    }
                | Gold ->
                    Ok {
                        Id = ItemIds.GoldIngot |> int
                        Name = "Gold Ingot"
                        Description = sprintf "A piece of gold ingot."
                        CraftRecipe = None
                        Material = Some Gold
                        Inventory = None
                        Quantity = quantity
                        Value = 20
                    }
                | _ ->
                    Error <| InvalidMaterial (string material)
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
                        Value = 3
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
                        Value = 5
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
                        Value = 15
                    }
                | _ ->
                    Error <| InvalidMaterial (string material)
        }