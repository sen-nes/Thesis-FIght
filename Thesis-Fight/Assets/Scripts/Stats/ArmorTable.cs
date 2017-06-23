using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorTable {

    private static float[,] table = { 
        { 1.05f, 0.7f, 1.75f, 1f, 0.5f }, 
        { 1.05f, 1.75f, 1f, 0.7f, 0.45f} , 
        { 1.05f, 1f, 0.7f, 1.75f, 0.4f }, 
        { 1f, 1f, 1f, 1f, 1f },
        { 1f, 0.7f, 0.7f, 0.7f, 1.6f },
        { 1.1f, 1.1f, 1.1f, 1.1f, 0.6f },
        { 1f, 1f, 1f, 1f, 1f }
    };

	public static float GetArmorModifier(DamageTypes damage, ArmorTypes armor)
    {
        return table[(int)damage, (int)armor];
    }

    public static float GetDamageReduction(float armor)
    {
        return (1f - (0.06f * armor / (1 + 0.06f * armor)));
    }
}
