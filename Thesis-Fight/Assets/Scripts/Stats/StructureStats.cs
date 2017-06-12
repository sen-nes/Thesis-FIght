using UnityEngine;

public class StructureStats : MonoBehaviour, IBaseStats
{
    // Base stats
    public Stat Health { get; set; }
    public Stat Armor { get; set; }
    public ArmorTypes ArmorType { get; set; }
} 
