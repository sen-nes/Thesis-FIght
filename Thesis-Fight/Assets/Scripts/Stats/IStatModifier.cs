using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatModifier
{
    string Name { get; set; }
    string Description { get; set; }
    float ModifierValue { get; set; }

    float Apply(float baseStat);
}
