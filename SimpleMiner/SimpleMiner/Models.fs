namespace Mining.Models

open Mining.Items

type Inventory = Item list

type Player = {
    Name: string
    Inventory: Inventory    
}