using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRegistry
{
    public static Dictionary<string, Item> itemRegistry = new();

    public static Item apple = GetItem("apple") ?? RegisterItem(new FoodItem("apple", new FoodItem.FoodProperties().SetHealthRegen(2).SetHungerRegen(2).SetMaxDurability(1).SetMaxStackSize(16)));
    public static Item air = GetItem("air") ?? RegisterItem(new("air", new Item.Properties()));

    private static Item RegisterItem(Item item)
    {
        if (item != null && !itemRegistry.ContainsKey(item.NameID))
        {
            itemRegistry.Add(item.NameID, item);
            return item;
        }

        return null;
    }

    public static Item GetItem(string nameID)
    {
        return itemRegistry.GetValueOrDefault(nameID, air);
    }
}