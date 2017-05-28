using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingStats : MonoBehaviour {

	public Stat Health { get; set; }
    public Stat Armor { get; set; }
    public int ArmorType { get; set; }

    private void Awake()
    {
        Health = new Stat("Health", "How much health the unit has.", 200);
        // Armor
        // ArmorType
    }
}
