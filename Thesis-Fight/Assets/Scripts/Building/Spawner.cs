using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject unit;
    public float spawnTime;

    // Check if it behaves as expected
    public float Progress
    {
        get
        {
            if (isSpawning)
            {
                return ((Time.time - spawnStart) % spawnTime) / spawnTime;
            }
            else
            {
                return 0f;
            }
        }
    }

    private Transform enemyCastle;
    private static Transform unitParent;
    private Transform spawnPoint;
    private bool isSpawning;
    private float spawnStart;

    private void Awake()
    {
        // Note: Path is mostly the same
        // Set unit final destination to EnemyCastle
        unitParent = GameObject.Find("Units").transform;
        spawnPoint = transform.Find("Spawn Point");
        isSpawning = false;

        // Setup reference to unit's spawnTime so it updates in-game?     
    }

    public void SpawnUnits()
    {
        // Set attackable instead
        unit.GetComponent<UnitController>().teamID = GetComponent<BuildingController>().teamID;
        unit.GetComponent<UnitController>().playerID = GetComponent<BuildingController>().playerID;
        unit.GetComponent<UnitController>().enemyCastle = GetComponent<BuildingController>().enemyCastle;
        unit.GetComponent<UnitController>().killValue = GetComponent<Attackable>().KillValue / 5;

        if (!Grid.instance.NodeFromPoint(spawnPoint.position).walkable)
        {
            spawnPoint.position = Grid.instance.FindFreeNode((Teams)GetComponent<BuildingController>().teamID, spawnPoint.position);
        }

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

    public Vector3 UpdateSpawnPoint()
    {
        spawnPoint.position = Grid.instance.FindFreeNode((Teams)GetComponent<BuildingController>().teamID, spawnPoint.position);

        return spawnPoint.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(spawnPoint.position, Vector3.one);
    }

    //public void UpdateGrid()
    //{
    //    Grid.instance.UpdateGridRegion(grid.sizeX, grid.sizeY, transform.position);
    //}

    // Create separate component from placemnet grid
}
