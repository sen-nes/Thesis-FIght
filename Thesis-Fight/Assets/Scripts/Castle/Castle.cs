using UnityEngine;

[CreateAssetMenu(fileName = "NewCastle", menuName = "Buildings/New castle...", order = 3)]
public class Castle : ScriptableObject {

    public float health;
    public float armor;
    public ArmorTypes armorType;

    public int killValue;

    private Priorities priority = Priorities.CASTLE;

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
