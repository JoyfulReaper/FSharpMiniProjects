namespace Mining.Inventory

open Mining.Items
open Mining.Models
open FsToolkit.ErrorHandling

type InventoryError =
    | ItemNotFound of string
    | InsufficientQuantity of string

module Inventory =
    let add itemId quantity (inventory:Inventory) : Inventory =
        let item = inventory |> List.tryFind (fun item -> item.Id = itemId)
        match item with
        | Some item -> 
            let newItem = { item with Quantity = item.Quantity + quantity }
            let newInventory = inventory |> List.filter (fun item -> item.Id <> itemId)
            newInventory @ [newItem]
            |> List.sortBy (fun item -> item.Name)
        | None ->
            let item = Item.create itemId quantity
            inventory @ [item]
            |> List.sortBy (fun item -> item.Name)
        
    let remove (itemId:int) quantity (inventory:Inventory) =
        result {
            let item = inventory |> List.tryFind (fun item -> item.Id = itemId)
            match item with
            | None ->
                return! Error (ItemNotFound "Item is not in inventory.")
            | Some item ->
                if item.Quantity < quantity then
                    return! Error (InsufficientQuantity "Insufficient quantity in inventory.")
                else if item.Quantity = quantity then
                    return! Ok (inventory |> List.filter (fun item -> item.Id <> itemId))
                else
                    let newItem = { item with Quantity = item.Quantity - quantity }
                    let newInventory = inventory |> List.filter (fun item -> item.Id <> itemId)
                    return! Ok (newInventory @ [newItem])
        }
        
        