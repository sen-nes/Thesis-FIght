using UnityEngine;

public class FighterStats : MonoBehaviour, IBaseStats
{
    // Base stats
    public Stat Health { get; set; }
    public Stat Armor { get; set; }
    public ArmorTypes ArmorType { get; set; }

    // Make it a float field
    public Stat Range { get; set; }
    public Stat AttackDamage { get; set; }
    public DamageTypes DamageType { get; set; }
    public Stat AttackSpeed { get; set; }
    public Stat CriticalChance { get; set; }
    public Stat CriticalDamage { get; set; }

    // Probably not necessary
    public Stat MovementSpeed { get; set; }
}
