using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public Stat Health { get; set; }
    public Stat MovementSpeed { get; set; }
    public Stat AttackSpeed { get; set; }
    public Stat AttackDamage { get; set; }
    public int DamageType { get; set; }
    public Stat Range { get; set; }
    public Stat CriticalChance { get; set; }
    public Stat CriticalDamage { get; set; }
    public Stat Armor { get; set; }
    public int ArmorType { get; set; }

    public DamageType UnitDamageType { get; set; }
    public ArmorType UnitArmorType { get; set; }

    private void Awake()
    {
        Health = new Stat("Health", "How much health the unit has.", 50);
        AttackDamage = new Stat("Attack damage", "How much damage the unit deals.", 10);
        AttackSpeed = new Stat("Attack speed", "How frequently the unit can attack.", 500);
        Range = new Stat("Range", "Distance the unit can attack from.", 100);
        Armor = new Stat("Armor", "Defense.", 5);
    }
}
