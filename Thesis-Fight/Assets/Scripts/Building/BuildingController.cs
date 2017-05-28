using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour, IBuilding {

    public GameObject unit;
    public Transform unitParent;

    // Unit property?
    public float spawnTime;

    // ???
    private bool isSpawning;

    private void Start()
    {
        unit = Resources.Load<GameObject>("Units/Unit East");
        unitParent = GameObject.Find("Units").transform;
        isSpawning = false;
    }

    public void SpawnUnit()
    {
        Instantiate(unit, transform.position, Quaternion.identity, unitParent);
    }

    public void StartSpawning()
    {
        InvokeRepeating("SpawnUnit", 2.0f, spawnTime);
        isSpawning = true;
    }
}
