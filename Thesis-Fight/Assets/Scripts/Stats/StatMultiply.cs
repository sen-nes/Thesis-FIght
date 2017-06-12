using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatMultiply : IStatModifier
{
    // Properties
    public string Name { get; set; }
    public float ModifierValue { get; set; }

    // Constructors
    public StatMultiply(string _name, string _description, float _modifierValue)
    {
        Name = _name;
        ModifierValue = _modifierValue;
    }

    // Methods
    public float Apply(float baseStat)
    {
        float modifiedValue = baseStat * ModifierValue;
        Debug.Log((baseStat * ModifierValue) + "/" + modifiedValue);

        return modifiedValue;
    }
}
