using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUnit", menuName = "Units/New unit...", order = 1)]
public class Unit : ScriptableObject {

    public string name;

    public float health;
    public float armor;
    public ArmorTypes armorType;

    public float range;
    public float attackDamage;
    public DamageTypes damageType;
    public float attackSpeed;
    public float criticalChance;
    public float criticalDamage;

    public float movementSpeed;

    private Priorities priority = Priorities.UNIT;

    public void Initialize(GameObject obj)
    {
        FighterStats stats = obj.GetComponent<FighterStats>();

        stats.Health = new Stat("Health", health);
        stats.Armor = new Stat("Armor", armor);
        stats.ArmorType = armorType;
        stats.Range = new Stat("Range", range);
        stats.AttackDamage = new Stat("Attack Damage", attackDamage);
        stats.DamageType = damageType;
        stats.AttackSpeed = new Stat("AttackSpeed", attackSpeed);
        stats.CriticalChance = new Stat("Critical Chance", criticalChance);
        stats.CriticalDamage = new Stat("Critical Damage", criticalDamage);
        stats.MovementSpeed = new Stat("Movement Speed", movementSpeed);

        Attackable attackable = obj.GetComponent<Attackable>();
        attackable.AttackPriority = priority;
    }
}
