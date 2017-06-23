using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour {

    // Generic array?
    public List<GameObject> buildings;
    public bool isBuilding;

    private bool onBuildingTask;
    private SelectionManager selectionManager;
    private InGameMenuManager inGameMenuManager;

    private Transform buildingsParent;
    private GameObject currentBuilding;

    private BuildingGrid buildingGrid;

    private Material[] buildingMaterials;

    // Set in inspector or set in code?
    public Material buildingOK;
    public Material buildingError;

    private int floorLayer;
    private int combatLayer;

    private void Awake()
    {
        isBuilding = false;
        onBuildingTask = false;

        // Ensure selection of correct objects
        selectionManager = GameObject.FindObjectOfType<SelectionManager>();
        inGameMenuManager = GameObject.FindObjectOfType<InGameMenuManager>();

        buildingsParent = GameObject.Find("Buildings").transform;

        // Find in a more appropriate way
        buildingGrid = GameObject.FindObjectOfType<BuildingGrid>();

        floorLayer = LayerMask.GetMask("Floor");
        combatLayer = LayerMask.NameToLayer("Combat");
    }

    
    private void Update()
    {
        if (onBuildingTask)
        {
            if (currentBuilding != null)
            {
                float distanceToBuilding = Vector3.Distance(transform.position, currentBuilding.transform.position);

                if (distanceToBuilding <= 6f)
                {
                    currentBuilding.SetActive(true);
                    currentBuilding = null;
                    onBuildingTask = false;

                    GetComponent<ClickAndMove>().CancelPath();
                }
            }
            else
            {
                CancelBuildingTask();
            }
        }

        if (!isBuilding)
        {
            if (selectionManager.selectedObject == this.gameObject && !inGameMenuManager.isOpen /*&& playerID == GameStartManager.HumanBuilderID*/)
            {
                if (Input.GetKeyUp(KeyCode.Q))
                {
                    BeginBuilding(0);
                }

                if (Input.GetKeyUp(KeyCode.W))
                {
                    BeginBuilding(1);
                }

                if (Input.GetKeyUp(KeyCode.E))
                {
                    BeginBuilding(2);
                }
            }
        }
    }

    private void StartBuildingTask()
    {
        Debug.Log("Started building task");
        Vector3 pos = currentBuilding.transform.position;
        GetComponent<ClickAndMove>().RequestPathToLocation(pos);

        buildingGrid.UpdateWorldGrid();
        currentBuilding.SetActive(false);
        onBuildingTask = true;
    }

    public void CancelBuildingTask()
    {
        Debug.Log("Stopped building task");
        if (onBuildingTask)
        {
            GoldManager.instance.AddGold(GetComponent<BuilderController>().playerID, currentBuilding.GetComponent<BuildingController>().building.cost);

            // Nullify velocity vector
            GetComponent<ClickAndMove>().CancelPath();

            // Check if there is a building currently being built
            Destroy(currentBuilding);
            currentBuilding = null;
            onBuildingTask = false;
            buildingGrid.UpdateWorldGrid();
        }
    }

    // Consider setting up a coroutine for building from HUD
    public void BeginBuilding(int index)
    {
        if (index >= buildings.Count || index < 0)
        {
            Debug.Log("Building does not exist.");
            return;
        }

        // Give correct values to GoldManager
        if (GoldManager.instance.HasGold(GetComponent<BuilderController>().playerID, buildings[index].GetComponent<BuildingController>().building.cost))
        {
            CancelBuildingTask();

            Vector3 buildPoint = GetBuildPoint();
            GameObject building = buildings[index];

            building.GetComponent<BuildingController>().teamID = GetComponent<BuilderController>().teamID;
            building.GetComponent<BuildingController>().playerID = GetComponent<BuilderController>().playerID;
            building.GetComponent<BuildingController>().enemyCastle = GetComponent<BuilderController>().enemyCastle;

            currentBuilding = Instantiate(building, buildPoint, Quaternion.identity, buildingsParent);

            // Renderer or MeshRenderer
            buildingMaterials = currentBuilding.GetComponentInChildren<Renderer>().materials;

            buildingGrid.Show();
            // Update material accordingly
            if (buildingGrid.UpdateGrid(currentBuilding.transform))
            {
                var materials = currentBuilding.GetComponentInChildren<Renderer>().materials;
                for (int i = 0; i < materials.Length; i++)
                {
                    materials[i] = buildingOK;
                }
            }
            else
            {
                var materials = currentBuilding.GetComponentInChildren<Renderer>().materials;
                for (int i = 0; i < materials.Length; i++)
                {
                    materials[i] = buildingError;
                }
            }
            
            isBuilding = true;
            StartCoroutine(MoveBuilding());
            // Notify menu
        }
        else
        {
            Debug.Log("Not enough gold.");
        }
    }

    public IEnumerator MoveBuilding()
    {
        while (true)
        {
            Vector3 buildPoint = GetBuildPoint();

            // Cache transform, consider setting up a check 
            // to see if any movement happened
            currentBuilding.transform.position = buildPoint;

            // Update material accordingly
            if (buildingGrid.UpdateGrid(currentBuilding.transform))
            {
                var materials = currentBuilding.GetComponentInChildren<Renderer>().materials;
                for (int i = 0; i < materials.Length; i++)
                {
                    materials[i] = buildingOK;
                }
            }
            else
            {
                var materials = currentBuilding.GetComponentInChildren<Renderer>().materials;
                for (int i = 0; i < materials.Length; i++)
                {
                    materials[i] = buildingError;
                }
            }

            if (Input.GetMouseButtonUp((int)MouseButton.MB_LEFT))
            {
                if (PlaceBuilding())
                {
                    yield break;
                }
            }

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                CancelBuilding();
                yield break;
            }

            yield return null;
        }
    }

    private bool PlaceBuilding()
    {
        // Check if player has enough money again

        if (buildingGrid.UpdateGrid(currentBuilding.transform))
        {
            // Hide grid and update world
            buildingGrid.Hide();

            // Renderer or MeshRenderer
            currentBuilding.GetComponentInChildren<Renderer>().materials = buildingMaterials;
            currentBuilding.layer = combatLayer;
            currentBuilding.GetComponent<Spawner>().StartSpawning();

            // Give correct playerID and building cost
            // should GoldManager be a global object or local to the builder
            GoldManager.instance.Pay(GetComponent<BuilderController>().playerID, currentBuilding.GetComponent<BuildingController>().building.cost);
            
            // currentBuilding = null;
            isBuilding = false;

            // Start task
            StartBuildingTask();

            // Notify menu
            return true;
        }
        else
        {
            // Log a notification if the player can't build at point
            Debug.Log("Can't build at this spot.");
            return false;
        }
    }

    private void CancelBuilding()
    {
        if (currentBuilding != null)
        {
            buildingGrid.Hide();
            Destroy(currentBuilding);
            currentBuilding = null;
        }

        isBuilding = false;
        // Notify menu
    }

    private Vector3 GetBuildPoint()
    {
        // Think of a neater way to ensure you are getting discernible feedback
        // Cache layer mask
        Vector3 buildPoint = Helpers.RaycastFloor(LayerMask.GetMask("Floor"));
        buildPoint = Grid.instance.NodeFromPoint(buildPoint).position;

        return buildPoint;
    }
}
