using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatAdd : IStatModifier
{

    // Properties
    public string Name { get; set; }
    public string Description { get; set; }
    public float ModifierValue { get; set; }

    // Constructors
    public StatAdd(string _name, string _description, float _modifierValue)
    {
        Name = _name;
        Description = _description;
        ModifierValue = _modifierValue;
    }

    // Methods
    public float Apply(float baseStat)
    {
        return ModifierValue;
    }
}
