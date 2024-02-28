using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using UnityEngine;

using static UnityEditor.Progress;

public class Inventory
{
    private readonly int _size;
    private readonly List<InventorySlot> _slots;

    public Inventory(int size)
    {
        _slots = new List<InventorySlot>(new InventorySlot[size]);
        _size = size;

        for (int slotIndex = 0; slotIndex < size; slotIndex++)
        {
            _slots[slotIndex] = new InventorySlot(ItemRegistry.air.DefaultStack);
        }
    }

    public bool AddItem(ItemStack item)
    {
        foreach (InventorySlot slot in _slots)
        {
            if (!slot.HasItem())
            {
                return slot.SetItem(item, true);
            }
            else if (slot.ItemStack.Equals(item) && slot.ItemStack.Size < slot.ItemStack.Item.MaxSize)
            {
                return slot.MergeItem(item);
            }
        }

        return false;
    }

    public bool SetItemAt(ItemStack item, int index)
    {
        return SetItemAt(item, index, false);
    }

    public bool SetItemAt(ItemStack item, int index, bool forceSet)
    {
        return _slots[index].SetItem(item, forceSet);
    }

    public ItemStack GetItemAt(int index)
    {
        return _slots[index].ItemStack;
    }

    public int SearchItem(ItemStack itemStack)
    {
        for (int slotIndex = 0; slotIndex < _slots.Count; slotIndex++)
        {
            InventorySlot slot = _slots[slotIndex];

            if (slot.ItemStack.Equals(itemStack))
            {
                return slotIndex;
            }
        }

        return -1;
    }
}

[System.Serializable]
public class InventorySlot
{
    private ItemStack _itemStack;
    public ItemStack ItemStack { get => _itemStack; }

    public InventorySlot(ItemStack item)
    {
        this._itemStack = item;
    }

    public bool HasItem()
    {
        return !_itemStack.Equals(ItemRegistry.air.DefaultStack);
    }

    public bool SetItem(ItemStack item)
    {
        return SetItem(item, false);
    }

    public bool SetItem(ItemStack item, bool forceSet)
    {

        if (!HasItem() || forceSet)
        {
            _itemStack = item;
            return true;
        }
        else
        {
            return false;
        }
    }


    public bool MergeItem(ItemStack item)
    {
        if (_itemStack.CanMergeItem(item))
        {
            _itemStack.IncreaseSize(item.Size);
            return true;
        }

        return false;
    }
}