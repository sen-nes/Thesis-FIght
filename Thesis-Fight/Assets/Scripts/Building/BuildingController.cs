using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour {

    //public Vector3 bounds;
    //public bool CanBuild
    //{
    //    get
    //    {
    //        return grid.canBuild;
    //    }
    //}

    //private Transform enemyCastle;

    ////public
    //public GameObject unit;
    //private static Transform unitParent;
    //private float unitSpawnTime;

    //// ?
    //private bool isSpawning;
    //// Pool
    //private PlacementGrid grid;
    //private Transform spawnPoint;

    //private BuildingSelectable selectable;
    //private BuldingDetails buildingDetails;

    //private float progress;

    //private void Awake()
    //{
    //    buildingDetails = GetComponent<BuldingDetails>();
    //    unitParent = GameObject.Find("Units").transform;

    //    unit = Resources.Load<GameObject>("Units/Footman");
    //    unitSpawnTime = unit.GetComponent<UnitDetails>().unitSpawnTime;

    //    // Note: Path is mostly the same
    //    if (buildingDetails.TeamID == 0)
    //    {
    //        enemyCastle = GameObject.Find("Castles").transform.GetChild(1).transform.Find("Attack Point");
    //    }
    //    else
    //    {
    //        enemyCastle = GameObject.Find("Castles").transform.GetChild(0).transform.Find("Attack Point");
    //    }

    //    unit.GetComponent<UnitMovement>().EnemyCastle = enemyCastle.position;
    //    unit.GetComponent<UnitDetails>().TeamID = buildingDetails.TeamID;
    //    unit.GetComponent<UnitDetails>().PlayerID = buildingDetails.PlayerID;

    //    isSpawning = false;
    //    grid = GetComponent<PlacementGrid>();

    //    bounds = GetComponent<Collider>().bounds.size;
    //    spawnPoint = transform.Find("Spawn Point");

    //    selectable = transform.Find("Selectable").GetComponent<BuildingSelectable>();
    //}

    //private void Update()
    //{
    //    if (isSpawning)
    //    {
    //        progress += Time.deltaTime;
    //    }

    //    if (selectable.Selected)
    //    {
    //        selectable.UpdateProgress(progress /unitSpawnTime);
    //    }
    //}

    //public void SpawnUnits()
    //{
    //    Instantiate(unit, spawnPoint.position, Quaternion.identity, unitParent);
    //    progress = 0.0f;
    //}

    //public void StartSpawning()
    //{
    //    // Use coroutine
    //    InvokeRepeating("SpawnUnits", unitSpawnTime, unitSpawnTime);
    //    progress = 0.0f;
    //    isSpawning = true;
    //}

    //public void StopSpawning()
    //{
    //    CancelInvoke();
    //    progress = 0.0f;
    //    isSpawning = false;
    //}

    //public void UpdateGrid()
    //{
    //    Grid.instance.UpdateGridRegion(grid.sizeX, grid.sizeY, transform.position);
    //}

    //public void ShowPlacementGrid()
    //{
    //    if (grid)
    //    {
    //        grid.Show();
    //    }
    //}

    //public void HidePlacementGrid()
    //{
    //    if(grid)
    //    {
    //        grid.Hide();
    //    }
    //}

    //public void DestroyPlacementGrid()
    //{
    //    Destroy(grid);
    //}
}
