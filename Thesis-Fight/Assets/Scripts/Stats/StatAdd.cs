using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatAdd : IStatModifier
{

    // Properties
    public string Name { get; set; }
    public string Description { get; set; }
    private int ModifierValue { get; set; }

    // Constructors
    public StatAdd(string _name, string _description, int _modifierValue)
    {
        Name = _name;
        Description = _description;
        ModifierValue = _modifierValue;
    }

    // Methods
    public int Apply(int baseStat)
    {
        return ModifierValue;
    }
}
