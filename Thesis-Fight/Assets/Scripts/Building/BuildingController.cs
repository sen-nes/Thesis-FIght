using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildingController : MonoBehaviour, IBuilding
{
    public int Cost { get; set; }
    public int IncomeValue { get; set; }
    public Stat Health { get; set; }
    public Stat Armor { get; set; }
    public ArmorType buildingArmorType { get; set; }
    public string Owner { get; set; }
    public bool IsSpecial { get; set; }

    public GameObject unit;
    private NavMeshObstacle obstacles;

    private void Awake()
    {
        InvokeRepeating("Spawn", 2f, 2f);
    }

    private void Spawn()
    {
        Instantiate(unit, transform.position + Vector3.one, Quaternion.identity, GameObject.Find("Units").transform);
    }
}
