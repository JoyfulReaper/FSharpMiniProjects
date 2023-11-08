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

type PositiveQuantity = private PositiveQuantity of int
module PositiveQuantity = 
    let create (quantity:int) =
        if quantity < 0 then
            Error <| InvalidQuantity quantity
        else
            Ok <| PositiveQuantity quantity

    let add (PositiveQuantity quantity) (PositiveQuantity quantityToAdd) =
         quantity + quantityToAdd |> create

    let subtract (PositiveQuantity quantity) (PositiveQuantity quantityToSubtract) =
        quantity - quantityToSubtract |> create

    let toInt (PositiveQuantity quantity) = quantity

type Inventory = Item list
and Item = {
    Id: int
    Name: string
    Description: string
    CraftRecipe: Item list option
    Material: Material option
    Inventory: Inventory option
    Quantity: PositiveQuantity
    Value: int
    Stackable: bool
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
            Stackable = true 
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
            Stackable = true 
        }
        
    let createIngot (material:Material) (quantity:PositiveQuantity) =
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
                        Stackable = true 
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
                        Stackable = true 
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
                        Stackable = true 
                    }
                | _ ->
                    Error <| InvalidMaterial (string material)
        }
        
    let createOre (material:Material) (quantity:PositiveQuantity) =
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
                        Stackable = true 
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
                        Stackable = true 
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
                        Stackable = true 
                    }
                | _ ->
                    Error <| InvalidMaterial (string material)
        }