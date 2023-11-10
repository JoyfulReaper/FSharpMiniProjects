module SimpleMiner.Game

open System
open Mining.Models
open Mining.Items
open Mining.Inventory

let goMining (player:Player) =
    let oreMaterials = [Material.Iron; Material.Copper]
    let chance = Random.Shared.Next(101)
    let ore = 
        if chance < 30 then
            Item.createOre oreMaterials.[0] 1
        else
            Item.createOre oreMaterials.[1] 1
            
    let inv = player.Inventory |> Inventory.add ore.Id ore.Quantity
    let player = { player with Inventory = inv }
    (ore, player)