using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

using static UnityEditor.Progress;

// This is the prefab item
public class Item
{
    private readonly string _nameID;
    private readonly Properties _properties;

    public string NameID { get => _nameID; }
    public Properties GProperties { get => _properties; }
    public ItemStack DefaultStack { get => new(this, 1); }
    public int Damage { get => GProperties.Damage; }
    public int MaxDurability { get => GProperties.MaxDurability; }
    public int MaxSize { get => GProperties.MaxSize; }
    public int Weight { get => GProperties.Weight; }

    public Item(string nameID, Properties properties)
    {
        _nameID = nameID;
        _properties = properties;
    }

    public bool OnUse()
    {
        return true;
    }

    public override bool Equals(object obj)
    {
        return obj is Item item && 
            NameID.Equals(item.NameID) &&
            GProperties.Equals(item._properties);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(NameID, GProperties);
    }

    //TODO: Add stuff like interact events

    public class Properties
    {
        private int _damage;
        private int _maxDurability;
        private int _maxSize;
        private int _weight;

        public int Damage { get => _damage; }
        public int MaxDurability { get => _maxDurability; }
        public int MaxSize { get => _maxSize; }
        public int Weight { get => _weight; }

        public Properties SetDamage(int damage)
        {
            _damage = damage;
            return this;
        }

        public Properties SetMaxDurability(int maxDurability)
        {
            _maxDurability = maxDurability;
            return this;
        }

        public Properties SetMaxStackSize(int maxSize)
        {
            _maxSize = maxSize;
            return this;
        }

        public Properties SetWeight(int weight)
        {
            _weight = weight;
            return this;
        }
        public override bool Equals(object obj)
        {
            return obj is Properties properties &&
                   _damage == properties._damage &&
                   _maxDurability == properties._maxDurability &&
                   _maxSize == properties._maxSize &&
                   _weight == properties._weight;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_damage, _maxDurability, _maxSize, _weight);
        }
    }
}

