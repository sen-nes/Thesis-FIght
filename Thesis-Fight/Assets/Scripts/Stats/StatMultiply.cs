using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatMultiply : IStatModifier
{
    // Properties
    public string Description { get; set; }
    public string Name { get; set; }
    private float ModifierValue { get; set; }

    // Constructors
    public StatMultiply(string _name, string _description, float _modifierValue)
    {
        Name = _name;
        Description = _description;
        ModifierValue = _modifierValue;
    }

    // Methods
    public int Apply(int baseStat)
    {
        int modifiedValue = (int)(baseStat * ModifierValue);
        Debug.Log((baseStat * ModifierValue) + "/" + modifiedValue);

        return modifiedValue;
    }
}
