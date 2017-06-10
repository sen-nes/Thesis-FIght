using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleStats : MonoBehaviour {

    public float health;
    public Stat Health { get; set; }
    public float armor;
    public Stat Armor { get; set; }
    public int ArmorType { get; set; }

    private void Awake()
    {
        Health = new Stat("Health", health);
        Armor = new Stat("Armor", armor);
        // ArmorType
    }
}
