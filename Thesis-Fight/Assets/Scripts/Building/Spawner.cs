using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject unit;

    // Check if it behaves as expected
    public float Progress
    {
        get
        {
            return (spawnStart % spawnTime) / spawnTime;
        }
    }

    private Transform enemyCastle;
    private static Transform unitParent;
    private Transform spawnPoint;
    private bool isSpawning;
    private float spawnTime;
    private float spawnStart;

    private void Awake()
    {
        // Note: Path is mostly the same
        // Set unit final destination to EnemyCastle
        unitParent = GameObject.Find("Units").transform;
        spawnPoint = transform.Find("Spawn Point");
        isSpawning = false;

        // Setup reference to unit's spawnTime so it updates in-game?
        spawnTime = unit.GetComponent<UnitController>().unit.spawnTime;        
    }

    public void SpawnUnits()
    {
        // Set attackable instead
        unit.GetComponent<UnitController>().teamID = GetComponent<BuildingController>().teamID;
        unit.GetComponent<UnitController>().playerID = GetComponent<BuildingController>().playerID;
        unit.GetComponent<UnitController>().enemyCastle = GetComponent<BuildingController>().enemyCastle;

        Instantiate(unit, spawnPoint.position, Quaternion.identity, unitParent);
    }

    public void StartSpawning()
    {
        // Setup invoke coroutine
        InvokeRepeating("SpawnUnits", spawnTime, spawnTime);
        spawnStart = Time.time;
        isSpawning = true;
    }

    public void StopSpawning()
    {
        CancelInvoke();
        spawnStart = 0.0f;
        isSpawning = false;
    }

    //public void UpdateGrid()
    //{
    //    Grid.instance.UpdateGridRegion(grid.sizeX, grid.sizeY, transform.position);
    //}
    
    // Create separate component from placemnet grid
}
