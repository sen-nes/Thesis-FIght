using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    // public UnitBase baseStats;

    public Stat Health { get; set; }
    public Stat MovementSpeed { get; set; }
    public Stat AttackSpeed { get; set; }
    public Stat AttackDamage { get; set; }
    [SerializeField]
    public DamageType unitDamageType;
    public Stat Range { get; set; }
    public Stat CriticalChance { get; set; }
    public Stat CriticalDamage { get; set; }
    public Stat Armor { get; set; }
    [SerializeField]
    public ArmorType UnitArmorType;
    
    private void Awake()
    {
        //Health = new Stat("Health", baseStats.health);
        //MovementSpeed = new Stat("Movement Speed", baseStats.movementSpeed);
        //AttackSpeed = new Stat("Attack Speed", baseStats.attackSpeed);
        //AttackDamage = new Stat("Attack Damage", baseStats.attackDamage);
        //Range = new Stat("Range", baseStats.range);
        //CriticalChance = new Stat("Critical Chance", baseStats.critChance);
        //CriticalDamage = new Stat("Critical Damage", baseStats.critDamage);
        //Armor = new Stat("Armor", baseStats.armor);
    }
}
