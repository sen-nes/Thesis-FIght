using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour, IBuilding {

    public GameObject unit;
    public bool CanBuild
    {
        get
        {
            return grid.canBuild;
        }
    }

    // Unit property?
    public float spawnTime;
    private static Transform unitParent;
    private Transform enemyCastle;

    public Vector3 bounds;
    

    // ???
    private bool isSpawning;
    private PlacementGrid grid;
    private Transform spawnPoint;

    private void Start()
    {
        // Path is mostly the same
        if (unitParent == null)
        {
            unitParent = GameObject.Find("Units").transform;
            if (GetComponent<Details>().teamID == 0)
            {
                enemyCastle = GameObject.Find("Castles").transform.GetChild(1).transform.Find("Attack Point");
            }
            else
            {
                enemyCastle = GameObject.Find("Castles").transform.GetChild(0).transform.Find("Attack Point");
            }

            unit.GetComponent<UnitMovement>().EnemyCastle = enemyCastle.position;
        }
        
        isSpawning = false;
        grid = GetComponent<PlacementGrid>();

        bounds = GetComponent<Collider>().bounds.size;
        spawnPoint = transform.Find("Spawn Point");
    }

    public void SpawnUnits()
    {
        Instantiate(unit, spawnPoint.position, Quaternion.identity, unitParent);
    }

    public void StartSpawning()
    {
        InvokeRepeating("SpawnUnits", 2.0f, spawnTime);
        isSpawning = true;
    }

    public void StopSpawning()
    {
        CancelInvoke();
        isSpawning = false;
    }

    public void UpdateGrid()
    {
        Grid.instance.UpdateGridRegion(grid.sizeX, grid.sizeY, transform.position);
    }

    public void ShowPlacementGrid()
    {
        if (grid)
        {
            grid.Show();
        }
    }

    public void HidePlacementGrid()
    {
        if(grid)
        {
            grid.Hide();
        }
    }

    public void DestroyPlacementGrid()
    {
        Destroy(grid);
    }
}
