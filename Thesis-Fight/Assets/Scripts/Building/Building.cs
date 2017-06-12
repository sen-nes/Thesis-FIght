using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuilding", menuName = "Buildings/New building...", order = 2)]
public class Building : ScriptableObject {

    public float health;
    public float armor;
    public ArmorTypes armorType;

    public int cost;
    public int killValue;

    private Priorities priority = Priorities.BUILDING;

    public void Initiate(GameObject obj)
    {
        StructureStats stats = obj.GetComponent<StructureStats>();

        stats.Health = new Stat("Health", health);
        stats.Armor = new Stat("Armor", armor);

        stats.ArmorType = armorType;

        Attackable attackable = obj.GetComponent<Attackable>();
        attackable.KillValue = killValue;
        attackable.AttackPriority = priority;
    }
}
