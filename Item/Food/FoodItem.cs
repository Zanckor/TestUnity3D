using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : Item
{
    public int HungerRegeneration { get => ((FoodProperties)GProperties).HungerRegeneration; }
    public int HealthRegeneration { get => ((FoodProperties)GProperties).HealthRegeneration; }

    public FoodItem(string nameID, Properties properties) : base(nameID, properties)
    {

    }

    public class FoodProperties : Properties
    {
        private int _hungerRegeneration;
        private int _healthRegeneration;

        public int HungerRegeneration { get => _hungerRegeneration; }
        public int HealthRegeneration { get => _healthRegeneration; }

        public FoodProperties SetHungerRegen(int hungerRegeneration)
        {
            _healthRegeneration = hungerRegeneration;
            return this;
        }

        public FoodProperties SetHealthRegen(int healthRegeneration)
        {
            _healthRegeneration = healthRegeneration;
            return this;
        }
    }
} 