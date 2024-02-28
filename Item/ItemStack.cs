using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

using static UnityEditor.Progress;

// This is like the instance of each item, with some custom data, so you just need to create a ItemStack from an const Item 
public class ItemStack
{
    private readonly Item _item;
    private int _size;
    private int _durability;

    public Item Item { get => _item; }
    public int Durability { get => _durability; }
    public int Size { get => _size; }



    //TODO: Add some other data like if its unbreakable, his custom name, etc

    public ItemStack(Item item)
    {
        _item = item;
        _size = item.MaxSize;
        _durability = item.MaxDurability;
    }

    public ItemStack(Item item, int size)
    {
        _item = item;
        _size = size;
        _durability = item.MaxDurability;
    }

    public bool CanMergeItem(ItemStack itemStack)
    {
        return itemStack.Equals(this) && _size + itemStack.Size <= _item.MaxSize;
    }

    public bool IncreaseSize(int quantity)
    {
        if (!(_size + quantity > _item.GProperties.MaxSize))
        {
            _size += quantity;
            return true;
        }

        return false;
    }

    public void DecreaseSize(int quantity)
    {
        _size -= quantity;
    }

    public bool SetSize(int quantity)
    {
        if (quantity <= _item.GProperties.MaxSize)
        {
            _size = quantity;
            return true;
        }

        return false;
    }

    public void ReduceDurability(int quantity)
    {
        if (_durability - quantity <= 0)
        {
            DecreaseSize(1);
        }
        else
        {
            _durability -= quantity;
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new();

        sb.AppendLine("Properties of " + _item.NameID);
        sb.AppendLine("Damage: " + _item.Damage);
        sb.AppendLine("Size: " + Size + " / " + _item.MaxSize);
        sb.AppendLine("Durability: " + Durability + " / " + _item.MaxDurability);
        
        if(_item is FoodItem)
        {
            sb.AppendLine("Health Regeneration: " + ((FoodItem)_item).HealthRegeneration);
            sb.AppendLine("Hunger Regeneration: " + ((FoodItem)_item).HungerRegeneration);
        }

        return sb.ToString();
    }

    public override bool Equals(object obj)
    {
        return obj is ItemStack stack &&
               _item.Equals(stack._item) &&
               _durability == stack._durability;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_item, _size, _durability);
    }
}